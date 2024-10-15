using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Data;
using TeacherTimetabler.Api.Models;
using TeacherTimetabler.Api.Security;
using TeacherTimetabler.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

// Register services
builder.Services.AddScoped<ClassService>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceOwnershipHandler>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register ASP.NET Core Identity with cookie-based authentication
builder
    .Services.AddIdentity<Teacher, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Configure the Identity cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/account/login"; // Redirect if not authenticated
    options.AccessDeniedPath = "/api/account/accessdenied"; // Redirect for access denied
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Cookie expiration time
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;

    // Disable redirects for API requests
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = ctx =>
        {
            // Return 401 instead of redirecting to the login page for APIs
            if (ctx.Request.Path.StartsWithSegments("/api"))
            {
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            }
            else
            {
                // Continue with the default behavior for non-API requests
                ctx.Response.Redirect(ctx.RedirectUri);
                return Task.CompletedTask;
            }
        },
        OnRedirectToAccessDenied = ctx =>
        {
            // Return 403 instead of redirecting for forbidden API access
            if (ctx.Request.Path.StartsWithSegments("/api"))
            {
                ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            }
            else
            {
                // Continue with the default behavior for non-API requests
                ctx.Response.Redirect(ctx.RedirectUri);
                return Task.CompletedTask;
            }
        },
    };
});

// Register authorization policies
builder
    .Services.AddAuthorizationBuilder()
    .AddPolicy(
        "ResourceOwner",
        policy =>
        {
            policy.AddRequirements(new ResourceOwnershipRequirement());
        }
    );

builder.Services.AddAutoMapper(typeof(Program));

// Add controllers
builder.Services.AddControllers();

// Add Swagger
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(
    async (context, next) =>
    {
        Console.WriteLine(
            $"Request method: {context.Request.Method}, Path: {context.Request.Path}"
        );
        await next.Invoke();
    }
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program { }
