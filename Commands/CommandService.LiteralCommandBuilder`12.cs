﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Bee.Commands
{
    internal partial class CommandService
    {
        private sealed class LiteralCommandBuilder
        <
            TParser1, TArgument1, TParserProperties1,
            TParser2, TArgument2, TParserProperties2,
            TParser3, TArgument3, TParserProperties3,
            TParser4, TArgument4, TParserProperties4
        > : ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3, TParser4, TArgument4, TParserProperties4>
            where TParser1 : IArgumentParser<TArgument1, TParserProperties1>
            where TParserProperties1 : struct
            where TArgument1 : notnull
            where TParser2 : IArgumentParser<TArgument2, TParserProperties2>
            where TParserProperties2 : struct
            where TArgument2 : notnull
            where TParser3 : IArgumentParser<TArgument3, TParserProperties3>
            where TParserProperties3 : struct
            where TArgument3 : notnull
            where TParser4 : IArgumentParser<TArgument4, TParserProperties4>
            where TParserProperties4 : struct
            where TArgument4 : notnull
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

            public ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3, TParser4, TArgument4, TParserProperties4> WithLiteral(string name, Action<ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3, TParser4, TArgument4, TParserProperties4>>? literal = null)
            {
                var builder = new LiteralCommandBuilder<
                    TParser1, TArgument1, TParserProperties1,
                    TParser2, TArgument2, TParserProperties2,
                    TParser3, TArgument3, TParserProperties3,
                    TParser4, TArgument4, TParserProperties4>(name);
                if (literal is not null)
                    literal(builder);
                _children.Add(builder.Build());
                return this;
            }

            public ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3, TParser4, TArgument4, TParserProperties4> WithArgument<TParser, TArgument, TParserProperties>(string name, TParser parser,
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

            public ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3, TArgument3, TParserProperties3, TParser4, TArgument4, TParserProperties4> Run(Action<TArgument1, TArgument2, TArgument3, TArgument4> @delegate)
            {
                _delegate = @delegate;
                return this;
            }
        }
    }
}