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

    public DatabaseInitializer(ApplicationDbContext context)
    {
        _context = context;
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
    }
}