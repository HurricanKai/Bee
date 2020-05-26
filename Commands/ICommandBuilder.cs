using System;

namespace Bee.Commands
{
    public interface ITypelessCommandBuilder
    {
        ICommandNode Build();
        ITypelessCommandBuilder WithLiteral(string name, Action<ITypelessCommandBuilder>? literal = null);

        ITypelessCommandBuilder WithArgument<TParser, TArgument, TParserProperties>(string name, TParser parser,
            TParserProperties? properties = null, Action<ITypelessCommandBuilder>? argument = null)
            where TParser : IArgumentParser<TArgument, TParserProperties>
            where TParserProperties : struct
            where TArgument : notnull;

        ITypelessCommandBuilder Run(Delegate @delegate);
    }

    public interface ICommandBuilder<
        TParser1, TArgument1, TParserProperties1,
        TParser2, TArgument2, TParserProperties2,
        TParser3, TArgument3, TParserProperties3,
        TParser4, TArgument4, TParserProperties4
    >
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
        ICommandNode Build();

        ICommandBuilder<TParser1, TArgument1, TParserProperties1,
                TParser2, TArgument2, TParserProperties2,
                TParser3, TArgument3, TParserProperties3,
                TParser4, TArgument4, TParserProperties4>
            WithLiteral(string name,
                Action<ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2,
                        TParserProperties2,
                        TParser3, TArgument3, TParserProperties3, TParser4, TArgument4, TParserProperties4>>? literal =
                    null);

        ICommandBuilder<TParser1, TArgument1, TParserProperties1,
                TParser2, TArgument2, TParserProperties2,
                TParser3, TArgument3, TParserProperties3,
                TParser4, TArgument4, TParserProperties4>
            WithArgument<TParser, TArgument, TParserProperties>(string name, TParser parser,
                TParserProperties? properties = null, Action<ITypelessCommandBuilder>? argument = null)
            where TParser : IArgumentParser<TArgument, TParserProperties>
            where TParserProperties : struct
            where TArgument : notnull;

        ICommandBuilder<TParser1, TArgument1, TParserProperties1,
            TParser2, TArgument2, TParserProperties2,
            TParser3, TArgument3, TParserProperties3,
            TParser4, TArgument4, TParserProperties4> Run(Action<TArgument1, TArgument2, TArgument3, TArgument4> @delegate);
    }

    public interface ICommandBuilder<
        TParser1, TArgument1, TParserProperties1,
        TParser2, TArgument2, TParserProperties2,
        TParser3, TArgument3, TParserProperties3
    >
        where TParser1 : IArgumentParser<TArgument1, TParserProperties1>
        where TParserProperties1 : struct
        where TArgument1 : notnull
        where TParser2 : IArgumentParser<TArgument2, TParserProperties2>
        where TParserProperties2 : struct
        where TArgument2 : notnull
        where TParser3 : IArgumentParser<TArgument3, TParserProperties3>
        where TParserProperties3 : struct
        where TArgument3 : notnull
    {
        ICommandNode Build();

        ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3,
            TArgument3, TParserProperties3> WithLiteral(string name,
            Action<ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2,
                TParser3, TArgument3, TParserProperties3>>? literal = null);

        ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2, TParser3,
            TArgument3, TParserProperties3> WithArgument<TParser, TArgument, TParserProperties>(string name,
            TParser parser, TParserProperties? properties = null,
            Action<ICommandBuilder<TParser, TArgument, TParserProperties, TParser1, TArgument1, TParserProperties1,
                TParser2, TArgument2, TParserProperties2,
                TParser3, TArgument3, TParserProperties3>>? argument = null)
            where TParser : IArgumentParser<TArgument, TParserProperties>
            where TParserProperties : struct
            where TArgument : notnull;
        
        ICommandBuilder<TParser1, TArgument1, TParserProperties1,
            TParser2, TArgument2, TParserProperties2,
            TParser3, TArgument3, TParserProperties3> Run(Action<TArgument1, TArgument2, TArgument3> @delegate);
    }

    public interface ICommandBuilder<
        TParser1, TArgument1, TParserProperties1,
        TParser2, TArgument2, TParserProperties2
    >
        where TParser1 : IArgumentParser<TArgument1, TParserProperties1>
        where TParserProperties1 : struct
        where TArgument1 : notnull
        where TParser2 : IArgumentParser<TArgument2, TParserProperties2>
        where TParserProperties2 : struct
        where TArgument2 : notnull
    {
        ICommandNode Build();

        ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2> WithLiteral(
            string name,
            Action<ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2>>?
                literal = null);

        ICommandBuilder<TParser1, TArgument1, TParserProperties1, TParser2, TArgument2, TParserProperties2>
            WithArgument<TParser, TArgument, TParserProperties>(string name,
                TParser parser, TParserProperties? properties = null,
                Action<ICommandBuilder<TParser, TArgument, TParserProperties, TParser1, TArgument1, TParserProperties1,
                    TParser2, TArgument2,
                    TParserProperties2>>? argument = null)
            where TParser : IArgumentParser<TArgument, TParserProperties>
            where TParserProperties : struct
            where TArgument : notnull;
        
        ICommandBuilder<TParser1, TArgument1, TParserProperties1,
            TParser2, TArgument2, TParserProperties2> Run(Action<TArgument1, TArgument2> @delegate);
    }

    public interface ICommandBuilder<
        TParser1, TArgument1, TParserProperties1
    >
        where TParser1 : IArgumentParser<TArgument1, TParserProperties1>
        where TParserProperties1 : struct
        where TArgument1 : notnull
    {
        ICommandNode Build();

        ICommandBuilder<TParser1, TArgument1, TParserProperties1> WithLiteral(string name,
            Action<ICommandBuilder<TParser1, TArgument1, TParserProperties1>>? literal = null);

        ICommandBuilder<TParser1, TArgument1, TParserProperties1> WithArgument<TParser, TArgument, TParserProperties>(
            string name,
            TParser parser, TParserProperties? properties = null,
            Action<ICommandBuilder<TParser, TArgument, TParserProperties, TParser1, TArgument1, TParserProperties1>>?
                argument = null)
            where TParser : IArgumentParser<TArgument, TParserProperties>
            where TParserProperties : struct
            where TArgument : notnull;
        
        
        ICommandBuilder<TParser1, TArgument1, TParserProperties1> Run(Action<TArgument1> @delegate);
    }

    public interface ICommandBuilder
    {
        ICommandNode Build();

        ICommandBuilder WithLiteral(string name,
            Action<ICommandBuilder>? literal = null);

        ICommandBuilder WithArgument<TParser, TArgument, TParserProperties>(string name,
            TParser parser, TParserProperties? properties = null,
            Action<ICommandBuilder<TParser, TArgument, TParserProperties>>? argument = null)
            where TParser : IArgumentParser<TArgument, TParserProperties>
            where TParserProperties : struct
            where TArgument : notnull;

        ICommandBuilder Run(Action @delegate);
    }
}