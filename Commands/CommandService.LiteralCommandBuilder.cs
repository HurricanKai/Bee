using System;
using System.Collections.Generic;
using System.Linq;

namespace Bee.Commands
{
    internal partial class CommandService
    {
        private sealed class LiteralCommandBuilder : ICommandBuilder
        {
            private string _name;
            private List<ICommandNode> _children = new List<ICommandNode>();
            private Delegate? _delegate;

            public LiteralCommandBuilder(string name)
            {
                _name = name;
            }

            public ICommandNode Build()
            {
                return new LiteralCommandNode(_name, _children, _delegate,null);
            }

            public ICommandBuilder WithLiteral(string name, Action<ICommandBuilder>? literal = null)
            {
                var builder = new LiteralCommandBuilder(name);
                if (literal is not null)
                    literal(builder);
                _children.Add(builder.Build());
                return this;
            }

            public ICommandBuilder WithArgument<TParser, TArgument, TParserProperties>(string name, TParser parser,
                TParserProperties? properties = null, Action<ICommandBuilder<TParser, TArgument, TParserProperties>>? argument = null)
                where TParser : IArgumentParser<TArgument, TParserProperties>
                where TArgument : notnull
                where TParserProperties : struct
            {
                var builder =
                    new ArgumentCommandBuilder<TParser, TArgument, TParserProperties>(name, parser, properties);
                if (argument is not null)
                    argument(builder);
                _children.Add(builder.Build());
                return this;
            }

            public ICommandBuilder Run(Action @delegate)
            {
                _delegate = @delegate;
                return this;
            }
        }
    }
}