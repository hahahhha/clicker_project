using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.UseCases.Boosts.UpdateBoost;

public class UpdateBoostCommandHandler : IRequestHandler<UpdateBoostCommand, int>
{
    private readonly IAppDbContext appDbContext;

    public UpdateBoostCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<int> Handle(UpdateBoostCommand request, CancellationToken cancellationToken)
    {
        var boost = await appDbContext
            .Boosts
            .FirstOrDefaultAsync(b => b.Id == request.BoostId);
        
        if (boost == null) throw new Exception($"Can not update boost with id: {request.BoostId} - boost was not found");

        boost.Title = request.UpdatedBoost.Title;
        boost.Price = request.UpdatedBoost.Price;
        boost.Profit = request.UpdatedBoost.Profit;
        boost.IsAuto = request.UpdatedBoost.IsAuto;
        boost.Image = request.UpdatedBoost.Image ?? new byte[] { };

        await appDbContext.SaveChangesAsync();
        
        return request.BoostId;
    }
}
