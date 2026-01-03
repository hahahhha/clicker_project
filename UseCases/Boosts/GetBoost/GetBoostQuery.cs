using System;
using MediatR;
using CSharpClicker.Dtos;

namespace CSharpClicker.UseCases.Boosts.GetBoost;

public record GetBoostQuery(int BoostId) : IRequest<BoostDto>;