using CSharpClicker.Domain;
using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.Intitialization;

public static class UserRolesInitializer
{
    public static async void SeedRolesAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        var roles = new[] { "User", "Admin" };

        foreach (var roleName in roles)
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new ApplicationRole { Name = roleName });       
    }
}

