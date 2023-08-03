namespace Ordering.Persistence.Models.ValueObjects;


public sealed record OrderItem(Guid ProductId, decimal Amount);
