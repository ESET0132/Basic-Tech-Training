using ProductApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register the repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add logging
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();


app.MapGet("/", () => "Product API is running! Use /api/products to access the products.");

app.MapControllers();

app.Run();