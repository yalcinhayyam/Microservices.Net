namespace Library.UnitOfWork;


public interface IHasDomainEvent 
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
    void AddDomainEvent(IDomainEvent @event);

}