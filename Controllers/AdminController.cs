using Microsoft.AspNetCore.Mvc;
using MediatR;
using CSharpClicker.UseCases.GetBoosts;
using CSharpClicker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using CSharpClicker.UseCases.Boosts.CreateBoost;
using CSharpClicker.UseCases.Boosts.DeleteBoost;
using CSharpClicker.Dtos;

namespace CSharpClicker.Controllers;

[Route("/")]
// [Authorize]
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

    [HttpPost("/admin/boost")]
    public async Task<IActionResult> Create([FromBody] BoostDto boostDto)
    {
        Console.WriteLine("created boost");
        
        // Создаем команду с DTO
        var command = new CreateBoostCommand(boostDto);
        
        await mediator.Send(command);
        return Ok();
    }

    [HttpDelete("/admin/boost/{id}")]
    public async Task<IActionResult> Delete(int id) 
    {
        var command = new DeleteBoostCommand(id);
        await mediator.Send(command);
        return Ok();
    }
}

