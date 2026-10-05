using API.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace API.Data;

public static class SeedDatabase
{
    public static async Task Initialize(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

        if (!roleManager.Roles.Any())
        {
            var customer = new AppRole { Name = "Customer" };
            var admin = new AppRole { Name = "Admin" };
            await roleManager.CreateAsync(customer);
            await roleManager.CreateAsync(admin);
        }

        if (!userManager.Users.Any())
        {
            var customer = new AppUser { Name = "Furkan Aras", UserName = "furkanaras", Email = "rsfurkan09@gmail.com" };
            var admin = new AppUser { Name = "Aras Furkan", UserName = "arasfurkan", Email = "rsf0900@gmail.com" };

            await userManager.CreateAsync(customer, "Customer_123");
            await userManager.AddToRoleAsync(customer, "Customer");

            await userManager.CreateAsync(admin, "Admin_123");
            await userManager.AddToRolesAsync(admin, ["Admin", "Customer"]);
        }
    }
}