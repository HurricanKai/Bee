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
using Serilog.Formatting.Json;
using SerilogKonsole.Sinks.SystemConsole.Themes;

namespace SerilogKonsole.Sinks.SystemConsole.Formatting
{
    class ThemedDisplayValueFormatter : ThemedValueFormatter
    {
        readonly IFormatProvider _formatProvider;

        public ThemedDisplayValueFormatter(KonsoleTheme theme, IFormatProvider formatProvider, ConcurrentWriter concurrentWriter)
            : base(theme, concurrentWriter)
        {
            _formatProvider = formatProvider;
        }

        public override ThemedValueFormatter SwitchTheme(KonsoleTheme theme)
        {
            return new ThemedDisplayValueFormatter(theme, _formatProvider, _concurrentWriter);
        }

        protected override int VisitScalarValue(ThemedValueFormatterState state, ScalarValue scalar)
        {
            if (scalar == null)
                throw new ArgumentNullException(nameof(scalar));
            return FormatLiteralValue(scalar, state.Output, state.Format);
        }

        protected override int VisitSequenceValue(ThemedValueFormatterState state, SequenceValue sequence)
        {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            var count = 0;

            using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                state.Output.Write('[');

            var delim = string.Empty;
            for (var index = 0; index < sequence.Elements.Count; ++index)
            {
                if (delim.Length != 0)
                {
                    using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                        state.Output.Write(delim);
                }

                delim = ", ";
                Visit(state, sequence.Elements[index]);
            }

            using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                state.Output.Write(']');

            return count;
        }

        protected override int VisitStructureValue(ThemedValueFormatterState state, StructureValue structure)
        {
            var count = 0;

            if (structure.TypeTag != null)
            {
                using (ApplyStyle(state.Output, KonsoleThemeStyle.Name, ref count))
                    state.Output.Write(structure.TypeTag);

                state.Output.Write(' ');
            }

            using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                state.Output.Write('{');

            var delim = string.Empty;
            for (var index = 0; index < structure.Properties.Count; ++index)
            {
                if (delim.Length != 0)
                {
                    using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                        state.Output.Write(delim);
                }

                delim = ", ";

                var property = structure.Properties[index];

                using (ApplyStyle(state.Output, KonsoleThemeStyle.Name, ref count))
                    state.Output.Write(property.Name);

                using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                    state.Output.Write('=');

                count += Visit(state.Nest(), property.Value);
            }

            using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                state.Output.Write('}');

            return count;
        }

        protected override int VisitDictionaryValue(ThemedValueFormatterState state, DictionaryValue dictionary)
        {
            var count = 0;

            using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                state.Output.Write('{');

            var delim = string.Empty;
            foreach (var element in dictionary.Elements)
            {
                if (delim.Length != 0)
                {
                    using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                        state.Output.Write(delim);
                }

                delim = ", ";

                using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                    state.Output.Write('[');

                using (ApplyStyle(state.Output, KonsoleThemeStyle.String, ref count))
                    count += Visit(state.Nest(), element.Key);

                using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                    state.Output.Write("]=");

                count += Visit(state.Nest(), element.Value);
            }

            using (ApplyStyle(state.Output, KonsoleThemeStyle.TertiaryText, ref count))
                state.Output.Write('}');

            return count;
        }

        public int FormatLiteralValue(ScalarValue scalar, TextWriter output, string format)
        {
            var value = scalar.Value;
            var count = 0;

            if (value == null)
            {
                using (ApplyStyle(output, KonsoleThemeStyle.Null, ref count))
                    output.Write("null");
                return count;
            }

            if (value is string str)
            {
                using (ApplyStyle(output, KonsoleThemeStyle.String, ref count))
                {
                    if (format != "l")
                        JsonValueFormatter.WriteQuotedJsonString(str, output);
                    else
                        output.Write(str);
                }
                return count;
            }

            if (value is ValueType)
            {
                if (value is int || value is uint || value is long || value is ulong ||
                    value is decimal || value is byte || value is sbyte || value is short ||
                    value is ushort || value is float || value is double)
                {
                    using (ApplyStyle(output, KonsoleThemeStyle.Number, ref count))
                        scalar.Render(output, format, _formatProvider);
                    return count;
                }

                if (value is bool b)
                {
                    using (ApplyStyle(output, KonsoleThemeStyle.Boolean, ref count))
                        output.Write(b);

                    return count;
                }

                if (value is char ch)
                {
                    using (ApplyStyle(output, KonsoleThemeStyle.Scalar, ref count))
                    {
                        output.Write('\'');
                        output.Write(ch);
                        output.Write('\'');
                    }
                    return count;
                }
            }

            using (ApplyStyle(output, KonsoleThemeStyle.Scalar, ref count))
                scalar.Render(output, format, _formatProvider);

            return count;
        }
    }
}