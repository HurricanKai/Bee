using System;
using System.Collections.Generic;
using System.Linq;

namespace Bee.Commands
{
    internal partial class CommandService
    {
        private sealed class LiteralCommandBuilder
        <
            TParser1, TArgument1, TParserProperties1
        > : ICommandBuilder<TParser1, TArgument1, TParserProperties1>
            where TParser1 : IArgumentParser<TArgument1, TParserProperties1>
            where TParserProperties1 : struct
            where TArgument1 : notnull
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

            public ICommandBuilder<TParser1, TArgument1, TParserProperties1> WithLiteral(string name, Action<ICommandBuilder<TParser1, TArgument1, TParserProperties1>>? literal = null)
            {
                var builder = new LiteralCommandBuilder<TParser1, TArgument1, TParserProperties1>(name);
                if (literal is not null)
                    literal(builder);
                _children.Add(builder.Build());
                return this;
            }

            public ICommandBuilder<TParser1, TArgument1, TParserProperties1> WithArgument<TParser, TArgument, TParserProperties>(string name, TParser parser,
                TParserProperties? properties = null, Action<ICommandBuilder<TParser, TArgument, TParserProperties, TParser1, TArgument1, TParserProperties1>>? argument = null)
                where TParser : IArgumentParser<TArgument, TParserProperties>
                where TArgument : notnull
                where TParserProperties : struct
            {
                var builder =
                    new ArgumentCommandBuilder<TParser, TArgument, TParserProperties, TParser1, TArgument1,
                        TParserProperties1>(name, parser, properties);
                if (argument is not null)
                    argument(builder);
                _children.Add(builder.Build());
                return this;
            }

            public ICommandBuilder<TParser1, TArgument1, TParserProperties1> Run(Action<TArgument1> @delegate)
            {
                _delegate = @delegate;
                return this;
            }
        }
    }
}