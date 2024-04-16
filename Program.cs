using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(Options => // valida con Cookies
        {Options.LoginPath = "/Usuario/Login)";
        Options.LoginPath = "Usuarios.Logout";
        Options.AccessDeniedPath = "/Home/Restringido";

        });
builder.Services.AddAuthorization(options =>
    {
        //options.AddPolicy("Empleado", policy =>policy.RequireClaim(ClaimTypes.Role, "Administrador", "Empleado"));
        options.AddPolicy("Administrador", policy =>policy.RequireRole(ClaimTypes.Role, "Administrador", "Administrador"));
    });
{
    
    return 0;
}

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
