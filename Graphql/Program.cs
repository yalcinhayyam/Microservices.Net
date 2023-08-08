using Graphql.Models;
using Shared.Common.Enums;
using Shared.Common.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();


builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();
app.MapGraphQL();
app.UseHttpsRedirection();
app.UseAuthorization();


app.Run();


public class Book
{
    public string Title { get; set; }

    public Author Author { get; set; }
}

public enum UserRole
{
    GUEST,
    DEFAULT,
    ADMINISTRATOR
}

public class Author
{
    public UserRole Role { get; set; }
    public string Name { get; set; }
}

public class Query
{
    public Product GetProdut() =>
    new Product
    {
        CreateAt = DateTime.Now,
        Title = "Patates",
        Id = Guid.NewGuid(),
        Prices = new List<Money> { new(Currencies.TL, 20.4m) },
        StockAmount = 200m,
        StockUnit = UnitType.Kg,
    };

    public Book GetBook() =>
        new Book
        {
            Title = "C# in depth.",
            Author = new Author
            {

                Name = "Jon Skeet"
            }
        };
}