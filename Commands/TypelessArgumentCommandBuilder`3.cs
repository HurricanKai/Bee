using System;
using System.Collections.Generic;
using System.Linq;

namespace Bee.Commands
{
    internal partial class CommandService
    {
        private sealed class TypelessArgumentCommandBuilder<
                TParser1, TArgument1, TParserProperties1
            >
            : ITypelessCommandBuilder
            where TParser1 : IArgumentParser<TArgument1, TParserProperties1>
            where TParserProperties1 : struct
            where TArgument1 : notnull
        {
            private string _name;
            private List<ICommandNode> _children = new List<ICommandNode>();
            private TParser1 _parser;
            private TParserProperties1? _properties;
            private Delegate? _delegate;

            public TypelessArgumentCommandBuilder(string name, TParser1 parser, TParserProperties1? properties = null)
            {
                _name = name;
                _parser = parser;
                _properties = properties;
            }

            public ICommandNode Build()
            {
                return new ArgumentCommandNode<TParser1, TArgument1, TParserProperties1>(_parser, _name, _children, _properties, _delegate,null);
            }

            public ITypelessCommandBuilder WithLiteral(string name, Action<ITypelessCommandBuilder>? literal = null)
            {
                var builder = new TypelessLiteralCommandBuilder(name);
                if (literal is not null)
                    literal(builder);
                _children.Add(builder.Build());
                return this;
            }

            public ITypelessCommandBuilder WithArgument<TParser, TArgument, TParserProperties>(string name, TParser parser,
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