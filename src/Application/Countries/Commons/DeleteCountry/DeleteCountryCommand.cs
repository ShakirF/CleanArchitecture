using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Countries.Commons.DeleteCountry;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Countries.Commons.DeleteCountry;

    public record DeleteCountryCommand(int id): IRequest { }

  
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteCountryCommandHandler(IApplicationDbContext context = null)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Countries.FindAsync(new object[] { request.id }, cancellationToken);
        if (entity == null)
        {

        }
        _context.Countries.Remove(entity);

        await _context.SaveChangesAsync();
        return Unit.Value;
    }
}