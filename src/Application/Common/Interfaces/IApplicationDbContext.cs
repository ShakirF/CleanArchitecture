
using CleanArchitecture.Domain.Entitises;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Country> Countries { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
