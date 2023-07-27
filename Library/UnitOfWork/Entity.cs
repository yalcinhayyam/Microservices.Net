
namespace Library.UnitOfWork;
public class Entity : IHasDomainEvent
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly ICollection<IDomainEvent> domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.ToList().AsReadOnly();

    protected Entity()
    {
    }

    protected Entity(Guid id) : this()
    {
        Id = id;

    }

    protected Entity(Guid id, DateTime createdAt) : this()
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