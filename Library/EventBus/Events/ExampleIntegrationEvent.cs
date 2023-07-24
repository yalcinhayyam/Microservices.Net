


using System.Text.Json.Serialization;
using Library.EventBus.Abstraction;

namespace Library.EventBus.Events;
public record ExampleIntegrationEvent(
   ) : IntegrationEvent
{

    [JsonInclude] public string Value { get; set; }
}

