using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bee.Commands;
using Konsole;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using SerilogKonsole;

namespace Bee
{
    class Program
    {
        static Task Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            Console.WindowWidth = Console.LargestWindowWidth / 2;
            Console.WindowHeight = Console.LargestWindowHeight / 2;
            
            return Host.CreateDefaultBuilder(args)
                .UseSerilog((context, configuration) =>
                    configuration
                        .Enrich.FromLogContext()
                        .Enrich.WithThreadId()
                        .Enrich.WithFormattedScope()
                        .WriteTo.Konsole(
                            outputTemplate: "{Timestamp:HH:mm:ss}|{Level:u3}|{ThreadId} {FormattedScope}=> {SourceContext}{NewLine}    {Message:lj}{NewLine}{Exception}",
                            concurrentWriter: new Window(Window.OpenBox("Logs", Console.WindowWidth, Console.WindowHeight - 1)).Concurrent(),
                            restrictedToMinimumLevel: LogEventLevel.Debug)
                        .WriteTo.Debug()
                        .WriteTo.File(new CompactJsonFormatter(), context.Configuration["Logging:File"])
                        .MinimumLevel.Verbose())
                .ConfigureServices(ConfigureServices)
                .RunConsoleAsync();
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddCommands();
            services.AddHostedService<CommandLineReader>();
        }
    }
}