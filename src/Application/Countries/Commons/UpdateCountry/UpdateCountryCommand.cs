using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Countries.Commons.CreateCountry;
using CleanArchitecture.Domain.Entitises;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Countries.Commons.UpdateCountry;

public class UpdateCountryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PhoneAreaCode { get; set; }
    }

public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCountryCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Countries.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
        {
            //TODO :
        }
        entity.Name = request.Name;
        entity.PhoneAreaCode = request.PhoneAreaCode;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}