using System;
using System.Linq;
using System.Text;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace Bee
{
    public class FormattedScopeEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var v = logEvent.Properties["Scope"].ToString() // Format: ["a", "b", "c"]
                .TrimStart('[').TrimEnd(']')
                .Split(',')
                .Select(x => x.Trim().TrimStart('"').TrimEnd('"'))
                .ToArray();
            var s = new StringBuilder();

            foreach (var x in v)
            {
                s.Append($"=> {x} ");
            }
                
                
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("FormattedScope", s.ToString(), true));
        }
    }

    public static class FormattedScopeEnricherExtenions
    {
        public static LoggerConfiguration WithFormattedScope(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<FormattedScopeEnricher>();
        }
    }
}