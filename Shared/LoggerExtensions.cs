using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Bee
{
    public static class LoggerExtensions
    {
        public static IDisposable BeginPropertyScope(this ILogger logger, params (string, object)[] properties)
        {
            return logger.BeginScope(properties.ToDictionary(x => x.Item1, x => x.Item2));
        }
        
        public static IDisposable BeginPropertyScope(this ILogger logger, string key, object value)
        {
            return logger.BeginScope(new Dictionary<string, object> {[key] = value});
        }
    }
}