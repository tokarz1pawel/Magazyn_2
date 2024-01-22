using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Magazyn.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

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

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Dodaj role, je�li nie istniej�
    var roles = new[] { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Adres e-mail u�ytkownika, kt�remu chcesz przypisa� rol�
    var userEmail = "admin1@example.com"; // Zast�p w�a�ciwym adresem e-mail

    // Nazwa roli, kt�r� chcesz przypisa� u�ytkownikowi
    var roleName = "Admin"; // Zast�p w�a�ciw� nazw� roli

    // Pobierz u�ytkownika po adresie e-mail
    var user = await userManager.FindByEmailAsync(userEmail);

    if (user != null)
    {
        // Sprawd�, czy u�ytkownik ju� ma przypisan� rol�
        var isInRole = await userManager.IsInRoleAsync(user, roleName);

        if (!isInRole)
        {
            // Przypisz rol� u�ytkownikowi
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Adres e-mail u�ytkownika, kt�remu chcesz przypisa� rol�
    var userEmail = "user@example.com"; // Zast�p w�a�ciwym adresem e-mail

    // Nazwa roli, kt�r� chcesz przypisa� u�ytkownikowi
    var roleName = "User"; // Zast�p w�a�ciw� nazw� roli

    // Pobierz u�ytkownika po adresie e-mail
    var user = await userManager.FindByEmailAsync(userEmail);

    if (user != null)
    {
        // Sprawd�, czy u�ytkownik ju� ma przypisan� rol�
        var isInRole = await userManager.IsInRoleAsync(user, roleName);

        if (!isInRole)
        {
            // Przypisz rol� u�ytkownikowi
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "products",
    pattern: "Products/{action=Index}/{id?}",
    defaults: new { controller = "Products" });

app.MapControllerRoute(
    name: "cart",
    pattern: "Cart/{action=Index}/{id?}",
    defaults: new { controller = "Cart" });


app.Run();
