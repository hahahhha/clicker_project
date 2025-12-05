using System;
using CSharpClicker.Infrastructure.Abstractions;
using CSharpClicker.Infrastructure.Implementations;
using MediatR;

namespace CSharpClicker.UseCases.Boosts.DeleteBoost;

public class DeleteBoostCommandHandler : IRequestHandler<DeleteBoostCommand, Unit>
{
    private readonly IAppDbContext appDbContext;

    public DeleteBoostCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(DeleteBoostCommand request, CancellationToken cancellationToken)
    {
        var toDeleteBoost = appDbContext.Boosts.FirstOrDefault(b => b.Id == request.DeleteId);
        if (toDeleteBoost is null)
        {
            throw new Exception($"пустой toDeleteBoost, {request.DeleteId}");
        }
        appDbContext.Boosts.Remove(toDeleteBoost);
        await appDbContext.SaveChangesAsync();
        return Unit.Value;
    }
}
