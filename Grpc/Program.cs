#define structType

using Grpc.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

#if  structType

app.MapGet("/", () =>
{
    Span<Money> prices = stackalloc Money[] { new Money() { Amount = 20, Currency = Currencies.Tl } };
    var product = new Product() { Prices = prices.ToArray(), Title = "Patates" };
    return Task.FromResult(product);;

});

#endif

#if !structType


app.MapGet("/", () =>
{
    var reply = new Product() { Title = "Patates", Prices = new Money[] { new Money() { Amount = 20, Currency = Currencies.Tl } } };
    return Task.FromResult(reply);

});

#endif


// app.MapGrpcService<Prod>();
app.MapGrpcService<ProductService>();
app.Run();


public sealed class Product
{
    public string Title { get; set; }
    public ICollection<Money> Prices { get; set; }
}

#if  structType
public struct Money
{
    public Currencies Currency { get; set; }
    public decimal Amount { get; set; }
}

# endif 


#if !structType
public class Money
{
    public Currencies Currency { get; set; }
    public decimal Amount { get; set; }
}

# endif 

public enum Currencies
{
    Tl = 0,
    Euro = 1,
    Dollar = 2,
}


/**
  "Kestrel": {
    "EndpointDefaults": {
      "Protocols": "Http2"
    }
  }

*/