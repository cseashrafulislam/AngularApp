using AngularApp1.Server.DB;
using AngularApp1.Server.Repositories.ProductRepo;
using AngularApp1.Server.Repositories;
using Microsoft.EntityFrameworkCore;
using AngularApp1.Server.Services;
using AngularApp1.Server.Repositories.CustomerRepo;
using AngularApp1.Server.Repositories.OrderRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin() // Allow all origins (adjust for security in production)
               .AllowAnyMethod() // Allow all methods (GET, POST, PUT, DELETE)
               .AllowAnyHeader(); // Allow all headers
    });
});

// Register the GenericRepository for all entities
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

// Optionally, add UnitOfWork if you are using it in your services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Register the specific ProductRepository (if needed)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDtlsRepository, OrderDtlsRepository>();

// Add ProductService for business logic
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<OrderService>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure JSON options to handle cycles
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64; // Optional: Increase max depth if needed
    });

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// If you are serving a front-end (e.g., Angular or React), you can map to the index.html for SPA
app.MapFallbackToFile("/index.html");

app.Run();
