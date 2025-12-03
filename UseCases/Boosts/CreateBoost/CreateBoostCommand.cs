using System;
using CSharpClicker.Domain;
using CSharpClicker.Dtos;
using MediatR;

namespace CSharpClicker.UseCases.Boosts.CreateBoost;

public record CreateBoostCommand(BoostDto BoostDto) : IRequest<Unit>;