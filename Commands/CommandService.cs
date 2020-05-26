using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Bee.Commands
{
    internal partial class CommandService : ICommandService
    {
        private readonly ICommandNode _root;
        private readonly ILogger _logger;
        
        public CommandService(ILogger<CommandService> logger, Action<ICommandBuilder> setup)
        {
            _logger = logger;
            var rootBuilder = new RootCommandBuilder();
            setup(rootBuilder);
            _root = rootBuilder.Build();
        }

        public void Execute(string command)
        {
            var sequence = new ReadOnlySequence<char>(command.AsMemory());
            var reader = new SequenceReader<char>(sequence);
            using (_logger.BeginScope("Executing Command"))
            using (_logger.BeginPropertyScope("Command", command))
            {
                var parameters = new List<object>();
                if (TryExecute(_root, ref reader, ref parameters))
                {
                    _logger.LogInformation("Successfully executed {Command} with {Remaining} chars to spare", command,
                        reader.Remaining);
                }
                else
                {
                    _logger.LogCritical("Failed to execute {Command}. {Remaining} chars remaining", command,
                        reader.Remaining);
                }
            }
        }

        private bool TryExecute(ICommandNode node, ref SequenceReader<char> sequenceReader, ref List<object> parameters)
        {
            var startParameterCount = parameters.Count;
            IDisposable? scope = null;

            if (node is INamedCommandNode nn)
            {
                scope = _logger.BeginScope($"{nn.Name}");
                _logger.LogDebug("Beginning to execute {Name}", nn.Name);
            }

            using (scope)
            {
                if (!node.Parse(ref sequenceReader, ref parameters))
                {
                    _logger.LogDebug("Failed to Parse");
                    var parameterCountDiff = parameters.Count - startParameterCount;
                    if (parameterCountDiff > 0)
                        parameters.RemoveRange(parameters.Count - parameterCountDiff, parameterCountDiff);
                    return false;
                }

                sequenceReader.AdvancePast(' ');

                var oldRemaining = sequenceReader.Remaining;
                foreach (var n in node.Children)
                {
                    if (TryExecute(n, ref sequenceReader, ref parameters))
                        return true;

                    var consumed = oldRemaining - sequenceReader.Remaining;
                    sequenceReader.Rewind(consumed);
                }
                
                // downstream didn't run, so this runs.
                if (node.Run is not null)
                {
                    try
                    {
                        node.Run.DynamicInvoke(parameters.ToArray());
                        return true;
                    }
                    catch (Exception e)
                    {
                        _logger.LogWarning(e, "Node {@Node} threw while running", node);
                    }
                }

                {
                    var parameterCountDiff = parameters.Count - startParameterCount;
                    if (parameterCountDiff > 0)
                        parameters.RemoveRange(parameters.Count - parameterCountDiff, parameterCountDiff);
                    return false;
                }
            }
        }
    }
}