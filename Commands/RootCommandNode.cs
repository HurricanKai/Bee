using System;
using System.Buffers;
using System.Collections.Generic;

namespace Bee.Commands
{
    public readonly struct RootCommandNode : ICommandNode
    {
        public RootCommandNode(IReadOnlyCollection<ICommandNode> children, Delegate? run = null)
        {
            Children = children;
            Run = run;
        }

        public IReadOnlyCollection<ICommandNode> Children { get; }
        public ICommandNode? Redirect => null;
        public bool Parse(ref SequenceReader<char> sequenceReader, ref List<object> parameters) => true;
        public Delegate? Run { get; }
    }
}