using System.Text;
using Library.ApiEndpoints.Implements;
using Library.Constants;
using Library.Data.Repositories.Implements;
using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Services.Implements;
using Library.Services.Interfaces;
using Library.Utils.Securities;
using Library.Utils.Securities.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


/*
 * The following code snippet configures the application to use the Swagger UI and database.
 * This is default configuration for the application.
 */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var configuration = builder.Configuration;

// Configure database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN"; // 
    options.Cookie.Name = "XSRF-TOKEN"; 
    options.Cookie.HttpOnly = false; 
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});



/*
 * This following code snippet configures the application to use all services.
 * These services will be injected into the API endpoints automatically.
 * You can add more services here.
 * Scoped services are created once per request, it is suitable for services that work with the database.
 */
// builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddSingleton<IConfiguration>(configuration);
// book
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
// book author
builder.Services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
// book category
builder.Services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
// author
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
// category
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// loan fine
builder.Services.AddScoped<ILoanFineService, LoanFineService>();
builder.Services.AddScoped<ILoanFineRepository, LoanFineRepository>();
// loan
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
// user
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// token invalid
builder.Services.AddScoped<ITokenInvalid, TokenInvalid>();
// bearer token
builder.Services.AddScoped<BearerToken>();

// builder.Services.AddScoped<IAuthorService, AuthorService>();

// ...


// application configuration
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true"); 
    });
    app.UseDeveloperExceptionPage();
}
// app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
// app.UseAuthorization();
app.Use(async (context, next) =>
{
    var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    var tokenInvalid = context.RequestServices.GetRequiredService<ITokenInvalid>();

    if (tokenInvalid.IsInvalid(token))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Token is invalid.");
        return;
    }
    await next();
});

// Apply migrations automatically
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     dbContext.Database.Migrate();
// }

/*
 * This following code is the API endpoints configuration.
 * You can add more endpoints here.
 */
var apiGroup = app.MapGroup(ApiPrefix.ApiVersion1);
// book
var bookEndpoint = new BookEndpoint();
bookEndpoint.DefineEndpoints(app, apiGroup);
// author
var authorEndpoint = new AuthorEndpoint();
authorEndpoint.DefineEndpoints(app, apiGroup);
// category
var categoryEndpoint = new CategoryEndpoint();
categoryEndpoint.DefineEndpoints(app, apiGroup);
// loan fine
var loanFineEndpoint = new LoanFineEndpoint();
loanFineEndpoint.DefineEndpoints(app, apiGroup);
// loan
var loanEndpoint = new LoanEndpoint();
loanEndpoint.DefineEndpoints(app, apiGroup);
// user
var userEndpoint = new UserEndpoint();
userEndpoint.DefineEndpoints(app, apiGroup);
// loan detail
var loanDetailEndpoint = new LoanDetailEndpoint();
loanDetailEndpoint.DefineEndpoints(app, apiGroup);



// Add authentication endpoints
// /auth/* (Ex: /auth/login, /auth/logout, /auth/antiforgery-token)
var authenticateApiGroup = app.MapGroup("/auth");

// antiforgery token
var antiForgeryEndpoint = new AntiForgeryEndpoint();
antiForgeryEndpoint.DefineEndpoints(app, authenticateApiGroup);
// authentication
var authentication = new Authentication();
authentication.DefineEndpoints(app, authenticateApiGroup);

app.Run();