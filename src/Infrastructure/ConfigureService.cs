using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entitises;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure;

    public static class ConfigureService
    {
    public static IServiceCollection AddINfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(configuration.GetConnectionString("local"), 
        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitalizer>();
        return services;
    }
    }

public class ApplicationDbContextInitalizer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<ApplicationDbContextInitalizer> _logger;

    public ApplicationDbContextInitalizer(ApplicationDbContext context, ILogger<ApplicationDbContextInitalizer> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitilazeAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error accurd while initializing the database");
            throw;
        }
    }
    public async Task TrySeedAsync()
    {
        var administratorRole = new IdentityRole("administrator");
        if(_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        var administrator = new ApplicationUser { UserName = "shakirfarajullayev@gmail.com", Email = "shakirfarajullayev@gmail.com" };
        if(_userManager.Users.All(u=>u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "administrator");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        if (!_context.Countries.Any())
        {
            await _context.Countries.AddRangeAsync(new List<Country>()
            {
                new Country {Name = "", PhoneAreaCode = ""}
            });
        }

        await _context.SaveChangesAsync();
    }

}
