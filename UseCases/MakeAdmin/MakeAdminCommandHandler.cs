using MediatR;
using Microsoft.AspNetCore.Identity;
using CSharpClicker.Domain;

namespace CSharpClicker.UseCases.MakeAdmin;

public class MakeAdminCommandController : IRequestHandler<MakeAdminCommand, Unit>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly IHttpContextAccessor httpContextAccessor;

    public MakeAdminCommandController(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IHttpContextAccessor httpContextAccessor)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(MakeAdminCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
        if (!await userManager.IsInRoleAsync(currentUser, "Admin"))
            throw new UnauthorizedAccessException("Только админы могут делать админов");

        var targetUser = await userManager.FindByIdAsync(request.UserId.ToString());
        if (targetUser == null) 
            throw new ArgumentException("Не удалось найти пользователя с указанным UserId");
        var currentRoles = await userManager.GetRolesAsync(targetUser);
        foreach (var role in currentRoles)
        {
            await userManager.RemoveFromRoleAsync(targetUser, role);
        }
        await userManager.AddToRoleAsync(targetUser, "Admin");

        return Unit.Value;
    }
}
