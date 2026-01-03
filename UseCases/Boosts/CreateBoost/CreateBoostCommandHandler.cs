using System;
using CSharpClicker.DomainServices;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using CSharpClicker.Domain;

namespace CSharpClicker.UseCases.Boosts.CreateBoost;

public class CreateBoostCommandHandler : IRequestHandler<CreateBoostCommand, Unit>
{
    private readonly IAppDbContext appDbContext;

    public CreateBoostCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(CreateBoostCommand request, CancellationToken cancellationToken)
    {
        var boostDto = request.BoostDto;
        appDbContext.Boosts.Add(new Boost
        {
            Title = boostDto.Title,
            Price = boostDto.Price,
            Profit = boostDto.Profit,
            IsAuto = boostDto.IsAuto,
            Image = boostDto.Image
        });
        await appDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
