using Domain.Workouts;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

internal sealed class ExerciseRepository(ApplicationDbContext context) : IExerciseRepository
{
    public void Insert(Exercise exercise)
    {
        context.Exercises.Add(exercise);
    }
}
