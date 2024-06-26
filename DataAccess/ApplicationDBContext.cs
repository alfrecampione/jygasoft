﻿using Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
     public ApplicationDbContext()
    {
        // Parameterless constructor
    }
    
    public DbSet<Person> Persons { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Person>()
            .HasKey(p => p.CI);
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Loan)
            .WithOne(l => l.Person)
            .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<Loan>()
            .HasKey(l=> l.Id);
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Person)
            .WithOne(p => p.Loan)
            .HasForeignKey<Loan>(l => l.PersonCI)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Loan>()
            .HasMany(l => l.Payments)
            .WithOne(p => p.Loan)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Payment>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Loan)
            .WithMany(l => l.Payments)
            .HasForeignKey(p => p.LoanId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); 
            //TODO: Check if this is correct, poder borrar el pago y recalcular los datos del prestamo
    }
}