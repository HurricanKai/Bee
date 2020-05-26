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

using System.Collections.Generic;
using System.IO;
using Konsole;
using Serilog.Events;
using Serilog.Parsing;
using SerilogKonsole.Sinks.SystemConsole.Rendering;
using SerilogKonsole.Sinks.SystemConsole.Themes;

namespace SerilogKonsole.Sinks.SystemConsole.Output
{
    class LevelTokenRenderer : OutputTemplateTokenRenderer
    {
        readonly KonsoleTheme _theme;
        readonly PropertyToken _levelToken;
        private readonly ConcurrentWriter _concurrentWriter;

        static readonly Dictionary<LogEventLevel, KonsoleThemeStyle> Levels = new Dictionary<LogEventLevel, KonsoleThemeStyle>
        {
            { LogEventLevel.Verbose, KonsoleThemeStyle.LevelVerbose },
            { LogEventLevel.Debug, KonsoleThemeStyle.LevelDebug },
            { LogEventLevel.Information, KonsoleThemeStyle.LevelInformation },
            { LogEventLevel.Warning, KonsoleThemeStyle.LevelWarning },
            { LogEventLevel.Error, KonsoleThemeStyle.LevelError },
            { LogEventLevel.Fatal, KonsoleThemeStyle.LevelFatal },
        };

        public LevelTokenRenderer(KonsoleTheme theme, PropertyToken levelToken, ConcurrentWriter concurrentWriter)
        {
            _theme = theme;
            _levelToken = levelToken;
            _concurrentWriter = concurrentWriter;
        }

        protected LevelTokenRenderer()
        {
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            var moniker = LevelOutputFormat.GetLevelMoniker(logEvent.Level, _levelToken.Format);
            if (!Levels.TryGetValue(logEvent.Level, out var levelStyle))
                levelStyle = KonsoleThemeStyle.Invalid;

            var _ = 0;
            using (_theme.Apply(output, levelStyle, ref _, _concurrentWriter))
                Padding.Apply(output, moniker, _levelToken.Alignment);
        }
    }
}