using Library.ApiEndpoints.Implements;
using Library.Constants;
using Library.DatabaseContext;
using Library.Services.Implements;
using Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (dependency injection)
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
// End of database configuration


// Services
builder.Services.AddScoped<IBookService, BookService>();

// End of services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Define API endpoints
var apiGroup = app.MapGroup(ApiPrefix.ApiVersion1);

var bookEndpoint = new BookEndpoint();
bookEndpoint.DefineEndpoints(app, apiGroup);

// End of API endpoints
app.Run();

