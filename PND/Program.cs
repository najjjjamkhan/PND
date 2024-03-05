using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using PND.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PNDContextConnection") ?? throw new InvalidOperationException("Connection string 'PNDContextConnection' not found.");

builder.Services.AddDbContext<PNDContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<PNDUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PNDContext>();

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
app.MapRazorPages();    

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Member" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<PNDUser>>();
    string email = "admin@gmail.com";
    string password = "Admin123@";
    if(await userManager.FindByEmailAsync(email)==null)
    {
        var user = new PNDUser();
        user.FirstName = "najam";
        user.LastName = "khan";
        user.UserName = email;
        user.Email = email;
        
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");


    }
}


app.Run();
