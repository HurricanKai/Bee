using System;
using System.Buffers;
using System.Collections.Generic;

namespace Bee.Commands
{
    public readonly struct LiteralCommandNode : INamedCommandNode
    {
        private readonly string _literal;

        public LiteralCommandNode(string literal, IReadOnlyCollection<ICommandNode> children, Delegate? run = null, ICommandNode? redirect = null)
        {
            _literal = literal;
            Children = children;
            Run = run;
            Redirect = redirect;
        }

        public IReadOnlyCollection<ICommandNode> Children { get; }
        public ICommandNode? Redirect { get; }

        public bool Parse(ref SequenceReader<char> sequenceReader, ref List<object> parameters)
            => sequenceReader.IsNext(_literal.AsSpan(), true);

        public Delegate? Run { get; }

        public string Name => _literal;
    }
}