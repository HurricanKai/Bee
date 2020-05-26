// Copyright 2017 Serilog Contributors
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
using System.IO;
using Konsole;
using Serilog.Data;
using Serilog.Events;
using SerilogKonsole.Sinks.SystemConsole.Themes;

namespace SerilogKonsole.Sinks.SystemConsole.Formatting
{
    abstract class ThemedValueFormatter : LogEventPropertyValueVisitor<ThemedValueFormatterState, int>
    {
        readonly KonsoleTheme _theme;
        protected readonly ConcurrentWriter _concurrentWriter;

        protected ThemedValueFormatter(KonsoleTheme theme, ConcurrentWriter concurrentWriter)
        {
            _theme = theme ?? throw new ArgumentNullException(nameof(theme));
            _concurrentWriter = concurrentWriter;
        }

        protected StyleReset ApplyStyle(TextWriter output, KonsoleThemeStyle style, ref int invisibleCharacterCount)
        {
            return _theme.Apply(output, style, ref invisibleCharacterCount, _concurrentWriter);
        }

        public int Format(LogEventPropertyValue value, TextWriter output, string format, bool literalTopLevel = false)
        {
            return Visit(new ThemedValueFormatterState { Output = output, Format = format, IsTopLevel = literalTopLevel }, value);
        }

        public abstract ThemedValueFormatter SwitchTheme(KonsoleTheme theme);
    }
}
