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
using Serilog.Events;
using Serilog.Parsing;
using SerilogKonsole.Sinks.SystemConsole.Rendering;
using SerilogKonsole.Sinks.SystemConsole.Themes;

namespace SerilogKonsole.Sinks.SystemConsole.Output
{
    class EventPropertyTokenRenderer : OutputTemplateTokenRenderer
    {
        readonly KonsoleTheme _theme;
        readonly PropertyToken _token;
        readonly IFormatProvider _formatProvider;
        private readonly ConcurrentWriter _concurrentWriter;

        public EventPropertyTokenRenderer(KonsoleTheme theme, PropertyToken token, IFormatProvider formatProvider, ConcurrentWriter concurrentWriter)
        {
            _theme = theme;
            _token = token;
            _formatProvider = formatProvider;
            _concurrentWriter = concurrentWriter;
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            // If a property is missing, don't render anything (message templates render the raw token here).
            if (!logEvent.Properties.TryGetValue(_token.PropertyName, out var propertyValue))
            {
                Padding.Apply(output, string.Empty, _token.Alignment);
                return;
            }

            var _ = 0;
            using (_theme.Apply(output, KonsoleThemeStyle.SecondaryText, ref _, _concurrentWriter))
            {
                var writer = _token.Alignment.HasValue ? new StringWriter() : output;

                // If the value is a scalar string, support some additional formats: 'u' for uppercase
                // and 'w' for lowercase.
                if (propertyValue is ScalarValue sv && sv.Value is string literalString)
                {
                    var cased = Casing.Format(literalString, _token.Format);
                    writer.Write(cased);
                }
                else
                {
                    propertyValue.Render(writer, _token.Format, _formatProvider);
                }

                if (_token.Alignment.HasValue)
                {
                    var str = writer.ToString();
                    Padding.Apply(output, str, _token.Alignment);
                }
            }
        }
    }
}