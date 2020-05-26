using System;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bee
{
    public class CommandLineReader : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IHost _host;
        private readonly ICommandService _commandService;

        public CommandLineReader(ILogger<CommandLineReader> logger, IHost host, ICommandService commandService)
        {
            _logger = logger;
            _host = host;
            _commandService = commandService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            var inputBuilder = new StringBuilder();
            var startPos = Console.CursorTop;
            var emptyLine = new string(' ', Console.WindowWidth);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.SetCursorPosition(0, startPos);
                inputBuilder.Clear();

                int writtenChars = 0;
                while (!stoppingToken.IsCancellationRequested)
                {
                    var c = await Task.Run(() => Console.ReadKey(true), stoppingToken);
                    
                    if (c.Key == ConsoleKey.Escape)
                    {
                        inputBuilder.Clear();
                        break;
                    }

                    // if (c.Key == ConsoleKey.Backspace) inputBuilder.Remove(inputBuilder.Length - 1, 1);
                    if (c.Key == ConsoleKey.Enter)
                        break;
                    inputBuilder.Append(c.KeyChar);
                    Console.Write(c.KeyChar);
                    writtenChars++;
                }

                if (stoppingToken.IsCancellationRequested)
                {
                    Console.SetCursorPosition(0, 0);
                    return;
                }

                while (Console.CursorTop > startPos)
                {
                    Console.CursorLeft = 0;
                    Console.Write(emptyLine);
                    Console.CursorTop -= 2;
                }
                Console.CursorLeft = 0;
                Console.Write(emptyLine);
                Console.SetCursorPosition(0, 0);
                
                var input = inputBuilder.ToString();

                if (string.IsNullOrWhiteSpace(input))
                    continue;
                
                if (input == "stop")
                {
                    break;
                }
                _logger.LogInformation($"User Input: {input}");
                _commandService.Execute(input);
            }
            
            _host.StopAsync();
        }
    }
}