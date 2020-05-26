using System;
using System.Collections.Generic;

namespace Bee.Commands
{
    internal partial class CommandService
    {
        private sealed class TypelessLiteralCommandBuilder : ITypelessCommandBuilder
        {
            private string _name;
            private List<ICommandNode> _children = new List<ICommandNode>();
            private Delegate? _delegate;

            public TypelessLiteralCommandBuilder(string name)
            {
                _name = name;
            }

            public ICommandNode Build()
            {
                return new LiteralCommandNode(_name, _children, _delegate,null);
            }

            public ITypelessCommandBuilder WithLiteral(string name, Action<ITypelessCommandBuilder>? literal = null)
            {
                var builder = new TypelessLiteralCommandBuilder(name);
                if (literal is not null)
                    literal(builder);
                _children.Add(builder.Build());
                return this;
            }

            public ITypelessCommandBuilder WithArgument<TParser, TArgument, TParserProperties>(string name,
                TParser parser,
                TParserProperties? properties = null, Action<ITypelessCommandBuilder>? argument = null)
                where TParser : IArgumentParser<TArgument, TParserProperties>
                where TArgument : notnull
                where TParserProperties : struct
            {
                var builder =
                    new TypelessArgumentCommandBuilder<TParser, TArgument, TParserProperties>(name, parser, properties);
                if (argument is not null)
                    argument(builder);
                _children.Add(builder.Build());
                return this;
            }

            public ITypelessCommandBuilder Run(Delegate @delegate)
            {
                _delegate = @delegate;
                return this;
            }
        }
    }
}