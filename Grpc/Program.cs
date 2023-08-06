var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
builder.Services.AddGrpc();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.MapGrpcService<Prod>();

app.Run();
