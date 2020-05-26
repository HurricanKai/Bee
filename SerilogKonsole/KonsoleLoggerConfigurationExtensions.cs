﻿// Copyright 2017 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Konsole;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using SerilogKonsole.Sinks.SystemConsole;
using SerilogKonsole.Sinks.SystemConsole.Output;
using SerilogKonsole.Sinks.SystemConsole.Themes;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.Console() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class KonsoleLoggerConfigurationExtensions
    {
        static object DefaultSyncRoot = new object();
        const string DefaultConsoleOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

        /// <summary>
        /// Writes log events to <see cref="System.Console"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// The default is <code>"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"</code>.</param>
        /// <param name="syncRoot">An object that will be used to `lock` (sync) access to the console output. If you specify this, you
        /// will have the ability to lock on this object, and guarantee that the console sink will not be about to output anything while
        /// the lock is held.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <param name="standardErrorFromLevel">Specifies the level at which events will be written to standard error.</param>
        /// <param name="theme">The theme to apply to the styled output. If not specified,
        /// uses <see cref="DefaultKonsoleTheme.Literate"/>.</param>
        /// <param name="applyThemeToRedirectedOutput">Applies the selected or default theme even when output redirection is detected.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration Konsole(
            this LoggerSinkConfiguration sinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = DefaultConsoleOutputTemplate,
            IFormatProvider formatProvider = null,
            LoggingLevelSwitch levelSwitch = null,
            LogEventLevel? standardErrorFromLevel = null,
            KonsoleTheme theme = null, 
            bool applyThemeToRedirectedOutput = false,
            object syncRoot = null,
            ConcurrentWriter concurrentWriter = null)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException(nameof(sinkConfiguration));
            if (outputTemplate == null) throw new ArgumentNullException(nameof(outputTemplate));
            
            concurrentWriter ??= new Window(Window.OpenBox("Logs")).Concurrent();

            var appliedTheme = !applyThemeToRedirectedOutput && (System.Console.IsOutputRedirected || System.Console.IsErrorRedirected) ?
                KonsoleTheme.None :
                theme ?? DefaultKonsoleThemes.Literate;

            syncRoot ??= DefaultSyncRoot;

            var formatter = new OutputTemplateRenderer(appliedTheme, outputTemplate, formatProvider, concurrentWriter);
            return sinkConfiguration.Sink(new KonsoleSink(appliedTheme, formatter, standardErrorFromLevel, syncRoot, concurrentWriter), restrictedToMinimumLevel, levelSwitch);
        }

        /// <summary>
        /// Writes log events to <see cref="System.Console"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="formatter">Controls the rendering of log events into text, for example to log JSON. To
        /// control plain text formatting, use the overload that accepts an output template.</param>
        /// <param name="syncRoot">An object that will be used to `lock` (sync) access to the console output. If you specify this, you
        /// will have the ability to lock on this object, and guarantee that the console sink will not be about to output anything while
        /// the lock is held.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <param name="standardErrorFromLevel">Specifies the level at which events will be written to standard error.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration Konsole(
            this LoggerSinkConfiguration sinkConfiguration,
            ITextFormatter formatter,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null,
            LogEventLevel? standardErrorFromLevel = null,
            object syncRoot = null,
            ConcurrentWriter concurrentWriter = null)
        {
            if (sinkConfiguration == null) throw new ArgumentNullException(nameof(sinkConfiguration));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            concurrentWriter ??= new Window(Window.OpenBox("Logs")).Concurrent();
            syncRoot ??= DefaultSyncRoot;
            return sinkConfiguration.Sink(new KonsoleSink(KonsoleTheme.None, formatter, standardErrorFromLevel, syncRoot, concurrentWriter), restrictedToMinimumLevel, levelSwitch);
        }
    }
}
