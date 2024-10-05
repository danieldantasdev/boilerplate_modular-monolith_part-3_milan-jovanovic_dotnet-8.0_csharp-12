using Domain.Users;
using Domain.Workouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.HasKey(w => w.Id);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(w => w.UserId)
            .IsRequired();

        builder.HasMany(w => w.Exercises)
            .WithOne()
            .HasForeignKey(e => e.WorkoutId)
            .IsRequired();
    }
}
