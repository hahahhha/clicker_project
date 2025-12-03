using Microsoft.AspNetCore.Mvc;
using MediatR;
using CSharpClicker.UseCases.GetBoosts;
using CSharpClicker.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CSharpClicker.Controllers;

[Route("/")]
[Authorize]
public class AdminController : Controller
{
    private readonly IMediator mediator;
    
    public AdminController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    // GET: AdminController
    [HttpGet("/admin")]
    public async Task<IActionResult> Admin()
    {
        var boosts = await mediator.Send(new GetBoostsQuery());
        var adminViewModel = new AdminViewModel
        {
            Boosts = boosts
        };

        return View(adminViewModel);
    }
}

