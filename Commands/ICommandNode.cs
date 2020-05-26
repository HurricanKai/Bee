using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bee.Commands
{
    public interface ICommandNode
    {
        IReadOnlyCollection<ICommandNode> Children { get; }
        ICommandNode? Redirect { get; }
        bool Parse(ref SequenceReader<char> sequenceReader, ref List<object> parameters);
        Delegate? Run { get; }
    }

    public interface INamedCommandNode : ICommandNode
    {
        string Name { get; }
    }
}