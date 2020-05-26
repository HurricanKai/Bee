using System;
using System.Buffers;
using System.Collections.Generic;

namespace Bee.Commands
{
    public readonly struct ArgumentCommandNode<TParser, TArgument, TParserProperties>
        : INamedCommandNode
        where TParser : IArgumentParser<TArgument, TParserProperties>
        where TParserProperties : struct
        where TArgument : notnull
    {
        private readonly TParser _parser;
        private readonly TParserProperties _parserProperties;

        public ArgumentCommandNode(TParser parser, string name, IReadOnlyCollection<ICommandNode> children, TParserProperties? parserProperties = null, Delegate? run = null, ICommandNode? redirect = null)
        {
            _parser = parser;
            _parserProperties = parserProperties ?? _parser.DefaultProperties;
            Name = name;
            Children = children;
            Run = run;
            Redirect = redirect;
        }

        public IReadOnlyCollection<ICommandNode> Children { get; }
        public ICommandNode? Redirect { get; }

        public bool Parse(ref SequenceReader<char> sequenceReader, ref List<object> parameters)
        {
            if (!_parser.Parse(_parserProperties, ref sequenceReader, out var argument))
                return false;
            parameters.Add(argument);
            return true;
        }

        public Delegate? Run { get; }

        public string Name { get; }
    }
}