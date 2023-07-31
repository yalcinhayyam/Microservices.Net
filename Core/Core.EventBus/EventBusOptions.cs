namespace Core.EventBus;

public record EventBusOptions
{
    public required string ConnectionString { get; set; }
    public required string QueueConnectionName { get; set; }

    public void Deconstruct(out string ConnectionString, out string QueueConnectionName)
    {
        ConnectionString = this.ConnectionString;
        QueueConnectionName = this.QueueConnectionName;
    }
}
