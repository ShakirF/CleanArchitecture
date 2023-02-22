using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entitises;
using CleanArchitecture.Infrastructure.Identity;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions options, 
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options,operationalStoreOptions)
        {

        }
        public DbSet<Country> Countries { get; set ; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
       
    }
}
