
using Library.UnitOfWork;

namespace Catalogue.Models.Events;

public sealed record ProductCreated(Guid id) : IDomainEvent;