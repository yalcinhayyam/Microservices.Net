using Core.Common.Domain;

namespace Catalogue.Domain.ValueObjects;

public sealed record ProductId(Guid Value) : AbstractEntityId(Value);