using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Countries.Queries;

public class GetCountriesQuery : IRequest<CountryVM>
    {
    }

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, CountryVM>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetCountriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CountryVM> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        return new CountryVM
        {
            Countries = await _context.Countries.AsNoTracking()
            .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
            .OrderBy(x=>x.Name)
            .ToListAsync()
        };
    }
}

