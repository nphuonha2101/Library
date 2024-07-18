using Library.ApiEndpoints.Implements;
using Library.Constants;
using Library.Data.Repositories.Implements;
using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Services.Implements;
using Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


/*
 * The following code snippet configures the application to use the Swagger UI and database.
 * This is default configuration for the application.
 */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


/*
 * This following code snippet configures the application to use all services.
 * These services will be injected into the API endpoints automatically.
 * You can add more services here.
 * Scoped services are created once per request, it is suitable for services that work with the database.
 */
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
// builder.Services.AddScoped<IAuthorService, AuthorService>();
// ...


// application configuration
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();


/*
 * This following code is the API endpoints configuration.
 * You can add more endpoints here.
 */
var apiGroup = app.MapGroup(ApiPrefix.ApiVersion1);

var bookEndpoint = new BookEndpoint();
bookEndpoint.DefineEndpoints(app, apiGroup);

// var authorEndpoint = new AuthorEndpoint();
// authorEndpoint.DefineEndpoints(app, apiGroup);

app.Run();

