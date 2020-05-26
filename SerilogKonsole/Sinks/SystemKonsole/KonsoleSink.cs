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
using System.Text;
using System.Threading.Tasks;
using Konsole;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using SerilogKonsole.Sinks.SystemConsole.Themes;

namespace SerilogKonsole.Sinks.SystemConsole
{
    class KonsoleSink : ILogEventSink
    {
        readonly LogEventLevel? _standardErrorFromLevel;
        readonly KonsoleTheme _theme;
        readonly ITextFormatter _formatter;
        readonly object _syncRoot;
        readonly ConcurrentWriter _concurrentWriter;
        readonly KonsoleTextWriter _writer;
        
        const int DefaultWriteBufferCapacity = 256;

        public KonsoleSink(
            KonsoleTheme theme,
            ITextFormatter formatter,
            LogEventLevel? standardErrorFromLevel,
            object syncRoot, ConcurrentWriter concurrentWriter)
        {
            _standardErrorFromLevel = standardErrorFromLevel;
            _theme = theme ?? throw new ArgumentNullException(nameof(theme));
            _formatter = formatter;
            _syncRoot = syncRoot ?? throw new ArgumentNullException(nameof(syncRoot));
            _concurrentWriter = concurrentWriter;
            _writer = new KonsoleTextWriter(_concurrentWriter);
        }

        public void Emit(LogEvent logEvent)
        {
            _formatter.Format(logEvent, _writer);
        }

        private class KonsoleTextWriter : TextWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
            private readonly ConcurrentWriter _concurrentWriter;

            public KonsoleTextWriter(ConcurrentWriter concurrentWriter)
            {
                _concurrentWriter = concurrentWriter;
            }

            public override void Write(char value) => Write(value.ToString());
            public override void Write(string value)
            {
                _concurrentWriter.Write(value);
            }

            public override void WriteLine()
            {
                _concurrentWriter.WriteLine("");
            }

            public override void WriteLine(string value)
            {
                _concurrentWriter.WriteLine(value);
            }
        }
    }
}
