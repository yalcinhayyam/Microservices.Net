
namespace Library.UnitOfWork;
public class Entity : IHasDomainEvent
{
    public int Id { get; protected set; }
    public DateTime DateOfCreate { get; protected set; }

    private readonly ICollection<IDomainEvent> domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.ToList().AsReadOnly();

    protected Entity()
    {
    }

    protected Entity(int id) : this()
    {
        Id = id;

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