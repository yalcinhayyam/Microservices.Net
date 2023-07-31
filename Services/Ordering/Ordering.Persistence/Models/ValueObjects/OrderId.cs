using Core.Common.Domain;

namespace Ordering.Persistence.Models.ValueObjects;

public sealed record OrderId(Guid Value): AbstractEntityId(Value);