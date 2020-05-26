using System;
using System.Buffers;

namespace Bee.Commands
{
    public static class SequenceReaderExtensions
    {
        public static ReadOnlySpan<char> ReadToSpaceOrEnd(ref this SequenceReader<char> reader)
        {
            ReadOnlySpan<char> span;
            
            if (reader.TryReadTo(out span, ' '))
            {
                return span;
            }

            span = reader.UnreadSequence.IsSingleSegment
                ? reader.UnreadSequence.FirstSpan
                : reader.UnreadSequence.ToArray();
            reader.AdvanceToEnd();
            return span;
        }
    }
}