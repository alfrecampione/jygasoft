using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InitialData;

public interface IDatabaseInitializer
{
    void EnsureInitialData();
}

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public DatabaseInitializer(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public void EnsureInitialData()
    {
        if (!AllMigrationsApplied()) throw new Exception("Not all migrations applied before seeding");

        SeedData();
    }


    private bool AllMigrationsApplied()
    {
        var applied = _context.GetService<IHistoryRepository>().GetAppliedMigrations().Select(m => m.MigrationId);
        var total = _context.GetService<IHistoryRepository>().GetAppliedMigrations().Select(m => m.MigrationId);
        return !total.Except(applied).Any();
    }


    private void SeedData()
    {
        // if (!_context.Set<Faculty>().Any())
        //     _context.Set<Faculty>().AddRange(InitialDomainData.GetFaculties());

        var roleSeeder = new RoleSeeder(_roleManager);
        roleSeeder.Seed().Wait();
        var userSeeder = new UserSeeder(_userManager, _roleManager);
        userSeeder.Seed().Wait();
    }
}

public class RoleSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleSeeder(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task Seed()
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            var admin = new IdentityRole { Name = "Admin" };
            await _roleManager.CreateAsync(admin);
        }

        if (!await _roleManager.RoleExistsAsync("Moderator"))
        {
            var moderator = new IdentityRole { Name = "Moderator" };
            await _roleManager.CreateAsync(moderator);
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            var user = new IdentityRole { Name = "User" };
            await _roleManager.CreateAsync(user);
        }
    }
}

public class UserSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public UserSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Seed()
    {
        if (await _userManager.FindByNameAsync("admin") == null)
        {
            var user = new IdentityUser { UserName = "admin" };
            var result = await _userManager.CreateAsync(user, "admin");
            if (result.Succeeded && await _roleManager.RoleExistsAsync("Admin"))
                await _userManager.AddToRoleAsync(user, "Admin");
        }

        if (await _userManager.FindByNameAsync("moderator") == null)
        {
            var user = new IdentityUser { UserName = "moderator" };
            var result = await _userManager.CreateAsync(user, "moderator");
            if (result.Succeeded && await _roleManager.RoleExistsAsync("Moderator"))
                await _userManager.AddToRoleAsync(user, "Moderator");
        }
    }
}