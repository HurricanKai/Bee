using System;
using System.Buffers;

namespace Bee.Commands
{
    public sealed class IntParser : IArgumentParser<int, IntParserProperties>
    {
        public static IntParser Shared { get; } = new IntParser();
        public IntParserProperties DefaultProperties => new IntParserProperties(int.MinValue, int.MaxValue);
        public bool Parse(IntParserProperties properties, ref SequenceReader<char> sequenceReader, out int argument)
        {
            var span = sequenceReader.ReadToSpaceOrEnd();
            return int.TryParse(span, out argument) && (argument <= properties.Max && argument >= properties.Min);
        }
    }

    public readonly struct IntParserProperties
    {
        public int Min { get; }
        public int Max { get; }

        public IntParserProperties(int min = int.MinValue, int max = int.MaxValue)
        {
            Min = min;
            Max = max;
        }
    }

    public static class IntParserExtensions
    {
        public static ITypelessCommandBuilder WithIntArgument(this ITypelessCommandBuilder builder, string name,
            IntParserProperties? properties = null,
            Action<ITypelessCommandBuilder>? argument = null)
            => builder.WithArgument<IntParser, int, IntParserProperties>(name, IntParser.Shared, properties, argument);
        
        public static ICommandBuilder WithIntArgument(this ICommandBuilder builder, string name,
            IntParserProperties? properties = null,
            Action<ICommandBuilder<IntParser, int, IntParserProperties>>? argument = null)
            => builder.WithArgument(name, IntParser.Shared, properties, argument);
        
        public static ICommandBuilder<TParser1, TArgument1, TParserProperties1> WithIntArgument<TParser1, TArgument1, TParserProperties1>(this ICommandBuilder<TParser1, TArgument1, TParserProperties1> builder, string name,
            IntParserProperties? properties = null,
            Action<ICommandBuilder<IntParser, int, IntParserProperties, TParser1, TArgument1, TParserProperties1>>? argument = null)
            where TParser1 : IArgumentParser<TArgument1, TParserProperties1>
            where TParserProperties1 : struct
            where TArgument1 : notnull
            => builder.WithArgument(name, IntParser.Shared, properties, argument);
        
        public static ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2> WithIntArgument<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2>(this ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2> builder, string name,
            IntParserProperties? properties = null,
            Action<ICommandBuilder<IntParser, int, IntParserProperties, TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2>>? argument = null)
            where TParser1 : IArgumentParser<TArgument1, TParserProperties1>
            where TParserProperties1 : struct
            where TArgument1 : notnull
            where TParser2 : IArgumentParser<TArgument2, TParserProperties2>
            where TParserProperties2 : struct
            where TArgument2 : notnull
            => builder.WithArgument(name, IntParser.Shared, properties, argument);
        
        public static ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3> WithIntArgument<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3>(this ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3> builder, string name,
            IntParserProperties? properties = null,
            Action<ICommandBuilder<IntParser, int, IntParserProperties, TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3>>? argument = null)
            where TParser1 : IArgumentParser<TArgument1, TParserProperties1>
            where TParserProperties1 : struct
            where TArgument1 : notnull
            where TParser2 : IArgumentParser<TArgument2, TParserProperties2>
            where TParserProperties2 : struct
            where TArgument2 : notnull
            where TParser3 : IArgumentParser<TArgument3, TParserProperties3>
            where TParserProperties3 : struct
            where TArgument3 : notnull
            => builder.WithArgument(name, IntParser.Shared, properties, argument);
        
        public static ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3, TParser4, TArgument4, TParserProperties4> WithIntArgument<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3, TParser4, TArgument4, TParserProperties4>(this ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3, TParser4, TArgument4, TParserProperties4> builder, string name,
            IntParserProperties? properties = null,
            Action<ITypelessCommandBuilder>? argument = null)
            where TParser1 : IArgumentParser<TArgument1, TParserProperties1>
            where TParserProperties1 : struct
            where TArgument1 : notnull
            where TParser2 : IArgumentParser<TArgument2, TParserProperties2>
            where TParserProperties2 : struct
            where TArgument2 : notnull
            where TParser3 : IArgumentParser<TArgument3, TParserProperties3>
            where TParserProperties3 : struct
            where TArgument3 : notnull
            where TParser4 : IArgumentParser<TArgument4, TParserProperties4>
            where TParserProperties4 : struct
            where TArgument4 : notnull
            => builder.WithArgument<IntParser, int, IntParserProperties>(name, IntParser.Shared, properties, argument);
    }
}