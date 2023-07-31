namespace Core.Common.Domain; 

public interface IRoot {}

public abstract record AbstractEntityId(Guid Value);

public class Entity<T> : IHasDomainEvent where T : AbstractEntityId
{
    public T Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly ICollection<IDomainEvent> domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.ToList().AsReadOnly();

    protected Entity()
    {
    }

    protected Entity(T id) : this()
    {
        Id = id;

    }

    protected Entity(T id, DateTime createdAt) : this()
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public void Created(DateTime createdAdt)
    {
        CreatedAt = createdAdt;
    }

    public void ClearDomainEvents()
    {
        domainEvents.Clear();

    }

    public void AddDomainEvent(IDomainEvent @event)
    {
        domainEvents.Add(@event);
    }
}