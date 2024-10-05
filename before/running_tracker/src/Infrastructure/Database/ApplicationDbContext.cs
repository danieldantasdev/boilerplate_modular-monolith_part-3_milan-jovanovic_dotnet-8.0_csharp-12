using System.Data;
using Application.Abstractions.Data;
using Domain.Activities;
using Domain.Followers;
using Domain.Users;
using Domain.Workouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users { get; set; }

    public DbSet<Follower> Followers { get; set; }

    public DbSet<Workout> Workouts { get; set; }

    public DbSet<Exercise> Exercises { get; set; }

    public DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public async Task<IDbTransaction> BeginTransactionAsync()
    {
        return (await Database.BeginTransactionAsync()).GetDbTransaction();
    }
}
