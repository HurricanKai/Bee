﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Bee.Commands
{
    internal partial class CommandService
    {
        private sealed class ArgumentCommandBuilder<TParser1, TArgument1, TParserProperties1>
            : ICommandBuilder<TParser1, TArgument1, TParserProperties1>
            where TParser1 : IArgumentParser<TArgument1, TParserProperties1>
            where TParserProperties1 : struct
            where TArgument1 : notnull
        {
            private string _name;
            private List<ICommandNode> _children = new List<ICommandNode>();
            private TParser1 _parser;
            private TParserProperties1? _properties;
            private Delegate? _delegate;

            public ArgumentCommandBuilder(string name, TParser1 parser, TParserProperties1? properties = null)
            {
                _name = name;
                _parser = parser;
                _properties = properties;
            }

            public ICommandNode Build()
            {
                return new ArgumentCommandNode<TParser1, TArgument1, TParserProperties1>(_parser, _name, _children, _properties, _delegate,null);
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