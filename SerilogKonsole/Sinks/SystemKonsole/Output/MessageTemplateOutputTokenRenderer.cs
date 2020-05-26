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
using SerilogKonsole.Sinks.SystemConsole.Formatting;
using SerilogKonsole.Sinks.SystemConsole.Rendering;
using SerilogKonsole.Sinks.SystemConsole.Themes;

namespace SerilogKonsole.Sinks.SystemConsole.Output
{
    class MessageTemplateOutputTokenRenderer : OutputTemplateTokenRenderer
    {
        readonly KonsoleTheme _theme;
        readonly PropertyToken _token;
        readonly ThemedMessageTemplateRenderer _renderer;

        public MessageTemplateOutputTokenRenderer(KonsoleTheme theme, PropertyToken token, IFormatProvider formatProvider, ConcurrentWriter concurrentWriter)
        {
            _theme = theme ?? throw new ArgumentNullException(nameof(theme));
            _token = token ?? throw new ArgumentNullException(nameof(token));
            bool isLiteral = false, isJson = false;

            if (token.Format != null)
            {
                for (var i = 0; i < token.Format.Length; ++i)
                {
                    if (token.Format[i] == 'l')
                        isLiteral = true;
                    else if (token.Format[i] == 'j')
                        isJson = true;
                }
            }

            var valueFormatter = isJson
                ? (ThemedValueFormatter)new ThemedJsonValueFormatter(theme, formatProvider, concurrentWriter)
                : new ThemedDisplayValueFormatter(theme, formatProvider, concurrentWriter);

            _renderer = new ThemedMessageTemplateRenderer(theme, valueFormatter, isLiteral, concurrentWriter);
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            _renderer.Render(logEvent.MessageTemplate, logEvent.Properties, output);
        }
    }
}