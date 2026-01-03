using CSharpClicker.Dtos;
using MediatR;

namespace CSharpClicker.UseCases.Boosts.UpdateBoost;

public record UpdateBoostCommand(BoostDto UpdatedBoost, int BoostId) : IRequest<int>;

