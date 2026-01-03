using System;
using AutoMapper;
using CSharpClicker.Dtos;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.UseCases.Boosts.GetBoost;

public class GetBoostQueryHandler : IRequestHandler<GetBoostQuery, BoostDto>
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBoostQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _dbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<BoostDto> Handle(GetBoostQuery request, CancellationToken cancellationToken)
    {
        var boost = await _dbContext.Boosts
            .FirstOrDefaultAsync(b => b.Id == request.BoostId, cancellationToken);

        if (boost == null) throw new Exception($"Boots was not found. Given id: {request.BoostId}");
        return _mapper.Map<BoostDto>(boost);
    }
}
