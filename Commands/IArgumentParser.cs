using System.Buffers;

namespace Bee.Commands
{
    public interface IArgumentParser<TArgument, TProperties>
        where TProperties : struct
        where TArgument : notnull
    {
        TProperties DefaultProperties { get; }

        bool Parse(TProperties properties, ref SequenceReader<char> sequenceReader, out TArgument argument);
    }
}