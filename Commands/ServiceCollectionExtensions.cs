using System;
using Bee.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bee
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection provider)
            => provider.AddSingleton<ICommandService>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("echo-run");
                return new CommandService(
                    serviceProvider.GetRequiredService<ILogger<CommandService>>(),
                    builder => builder
                        .WithLiteral("echo", builder => builder
                            .WithIntArgument("number", new IntParserProperties(0), builder => builder
                                .WithLiteral("literal", builder => builder
                                    .Run(i => logger.LogInformation($"Ran echo {i} literal", i))))));
            });
    }
}