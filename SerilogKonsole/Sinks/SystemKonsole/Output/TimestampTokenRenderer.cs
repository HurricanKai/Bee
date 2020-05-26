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
    class TimestampTokenRenderer : OutputTemplateTokenRenderer
    {
        readonly KonsoleTheme _theme;
        readonly PropertyToken _token;
        readonly IFormatProvider _formatProvider;
        private readonly ConcurrentWriter _concurrentWriter;

        public TimestampTokenRenderer(KonsoleTheme theme, PropertyToken token, IFormatProvider formatProvider, ConcurrentWriter concurrentWriter)
        {
            _theme = theme;
            _token = token;
            _formatProvider = formatProvider;
            _concurrentWriter = concurrentWriter;
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            // We need access to ScalarValue.Render() to avoid this alloc; just ensures
            // that custom format providers are supported properly.
            var sv = new ScalarValue(logEvent.Timestamp);

            var _ = 0;
            using (_theme.Apply(output, KonsoleThemeStyle.SecondaryText, ref _, _concurrentWriter))
            {
                if (_token.Alignment == null)
                {
                    sv.Render(output, _token.Format, _formatProvider);
                }
                else
                {
                    var buffer = new StringWriter();
                    sv.Render(buffer, _token.Format, _formatProvider);
                    var str = buffer.ToString();
                    Padding.Apply(output, str, _token.Alignment);
                }
            }
        }
    }
}
