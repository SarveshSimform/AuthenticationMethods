using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.Cookie.Name = "LoginCookie";
    opt.Cookie.Path = "/";
    opt.Cookie.Domain = "localhost";
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    opt.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    opt.SlidingExpiration = true;

    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Home/Error";
    opt.ReturnUrlParameter = "ReturnUrl";

    //opt.Events = new CookieAuthenticationEvents()
    //{
    //    OnRedirectToAccessDenied = (c) =>
    //    {
    //        c.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    //        c.Response.Redirect("/Home/Index");
    //        return Task.CompletedTask;
    //    }
    //};
});

//------------------------------ POLICIES -----------------------------------------
builder.Services.AddAuthorization(options =>
{
    // Policy for Admin role
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});
//------------------------------------------------------------------------------------

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
