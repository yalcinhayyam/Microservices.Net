
using Catalogue.Domain.ValueObjects;
using Core.Common.Domain;

namespace Catalogue.Domain.Events;

public sealed record ProductCreated(ProductId id) : IDomainEvent;