using CSharpClicker.Domain;
using MediatR;
using CSharpClicker.Dtos;
using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.UseCases.GetAllUsers;

public record GetAllUsersQuery() : IRequest<List<ApplicationUserWithRole>>;