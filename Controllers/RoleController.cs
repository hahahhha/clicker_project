using Microsoft.AspNetCore.Mvc;
using CSharpClicker.UseCases.MakeAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using CSharpClicker.UseCases.GetAllUsers;
using CSharpClicker.ViewModels;

namespace CSharpClicker.Controllers;

[Route("role")]
[Authorize(Roles = "Admin")] 
public class RoleController : Controller
{
    private readonly IMediator mediator;
    
    public RoleController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("")]
    public async Task<IActionResult> Roles()
    {
        var users = await mediator.Send(new GetAllUsersQuery());
        var viewModel = new RolesViewModel()
        {
            Users = users
        };
        return View(viewModel);
    }

    [HttpPost("admin/{id}")]
    public async Task<IActionResult> MakeAdmin(Guid id)
    {
        await mediator.Send(new MakeAdminCommand(id));
        return Ok();
    }

    [HttpPost("user/{id}")]
    public async Task<IActionResult> RemoveAdmin(Guid id)
    {
        return Ok();
    }
}