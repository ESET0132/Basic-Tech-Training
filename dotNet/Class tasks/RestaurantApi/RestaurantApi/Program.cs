using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.DbContext;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
        var created = context.Database.EnsureCreated();
        Console.WriteLine($" Database created: {created}");
        Console.WriteLine(" Database 'MyRestaurant' and tables created successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($" Error creating database: {ex.Message}");
    }
}

app.Run();