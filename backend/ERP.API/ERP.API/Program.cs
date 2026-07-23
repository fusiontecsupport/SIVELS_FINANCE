using ERP.API.Data;
using ERP.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database Configuration
builder.Services.AddDbContext<ERPDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        });
});


// CORS Configuration for React Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Add Controllers
builder.Services.AddControllers();


// Dependency Injection Services
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<PermissionService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IOtpService, OtpService>();


// OpenAPI / Swagger
builder.Services.AddOpenApi();


var app = builder.Build();


// Configure HTTP Pipeline

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();


// Enable CORS
app.UseCors("ReactPolicy");


// Authentication (enable later if using JWT)
// app.UseAuthentication();


// Authorization
app.UseAuthorization();


// Map API Controllers
app.MapControllers();


app.Run();