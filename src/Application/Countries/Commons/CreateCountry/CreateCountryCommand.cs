using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entitises;
using MediatR;

namespace CleanArchitecture.Application.Countries.Commons.CreateCountry;

public class CreateCountryCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public string? PhoneAreaCode { get; set; }
}
public class CreateCountryComandHandler : IRequestHandler<CreateCountryCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateCountryComandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }
    public async Task<int> Handle(CreateCountryCommand request,CancellationToken cancellationToken)
    {
       Country entity = new()
        {
           Name = request.Name,
           PhoneAreaCode = request.PhoneAreaCode,
        };
        await _context.Countries.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }
}