using System;
using MediatR;

namespace CSharpClicker.UseCases.Boosts.DeleteBoost;

public record DeleteBoostCommand(int DeleteId) : IRequest<Unit>;