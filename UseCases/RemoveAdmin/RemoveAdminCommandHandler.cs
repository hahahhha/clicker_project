using System;
using CSharpClicker.Domain;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.UseCases.RemoveAdmin;

public class RemoveAdminCommandHandler : IRequestHandler<RemoveAdminCommand, Guid>
{
    private readonly IAppDbContext appDbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IHttpContextAccessor httpContextAccessor;

    public RemoveAdminCommandHandler(
        IAppDbContext appDbContext, 
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        this.appDbContext = appDbContext;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> Handle(RemoveAdminCommand request, CancellationToken cancellationToken)
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
        await userManager.AddToRoleAsync(targetUser, "User");
        return targetUser.Id;
    }
}
