using CSharpClicker.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CSharpClicker.Infrastructure.Abstractions;
using CSharpClicker.Domain;
using System.Linq;

namespace CSharpClicker.UseCases.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<ApplicationUserWithRole>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IAppDbContext appDbContext;

    public GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager, IAppDbContext appDbContext)
    {
        this.userManager = userManager;
        this.appDbContext = appDbContext;
    }

    public async Task<List<ApplicationUserWithRole>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
        var result = new List<ApplicationUserWithRole>();
        foreach (var user in users)
        {
            var role = await userManager.GetRolesAsync(user);
            var userWithRole = new ApplicationUserWithRole()
            {
                UserName = user.UserName,
                Id = user.Id,
                Role = role.FirstOrDefault() ?? "User"
            };
            result.Add(userWithRole);
        }
        return result;
    }
}
