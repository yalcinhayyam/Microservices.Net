
record Order
{
    public Guid Id { get; set; }
    private readonly ICollection<Item> items;
    public IReadOnlyCollection<Item> Items => items.ToList().AsReadOnly();
    public Order(Guid id)
    {
        Id = id;
        items = new HashSet<Item>();
    }


    public void Add(Item item)
    {
        var product = items.FirstOrDefault(p => p.Product.Id == item.Product.Id);
        if (product is not null)
        {
            items.Remove(product);
            items.Add(product with { Amount = product.Amount + item.Amount });
            return;
        }
        items.Add(item);
    }


    public void Remove(Item item)
    {
        items.Remove(item);
    }
}


record Item(Product Product, int Amount);


enum Statuses
{
    /// <summary>
    /// Pending: The order has been received and is awaiting processing.
    /// Processing: The order is being processed and prepared for shipment.
    /// Shipped: The order has been shipped and is in transit to the customer.
    /// Delivered: The order has been successfully delivered to the customer.
    /// Cancelled: The order has been canceled, either by the customer or the merchant.
    /// On Hold: The order is temporarily on hold for various reasons, such as payment issues or product availability.
    /// Returned: The order has been returned by the customer.
    /// </summary>
    Pending, Processing, Shipped, Delivered, Cancelled, OnHold, Returned
}



