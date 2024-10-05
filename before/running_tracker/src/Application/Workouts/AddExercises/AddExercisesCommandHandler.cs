using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Workouts;
using SharedKernel;

namespace Application.Workouts.AddExercises;

internal sealed class AddExercisesCommandHandler(
    IWorkoutRepository workoutRepository,
    IExerciseRepository exerciseRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddExercisesCommand>
{
    public async Task<Result> Handle(
        AddExercisesCommand request,
        CancellationToken cancellationToken)
    {
        Workout? workout = await workoutRepository.GetByIdAsync(
            request.WorkoutId,
            cancellationToken);

        if (workout is null)
        {
            return Result.Failure(WorkoutErrors.NotFound(request.WorkoutId));
        }

        var results = request
            .Exercises
            .Select(exercise => workout.AddExercise(
                exercise.ExerciseType,
                exercise.TargetType,
                exercise.DistanceInMeters,
                exercise.DurationInSeconds))
            .ToList();

        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure(ValidationError.FromResults(results));
        }

        foreach (Exercise exercise in workout.Exercises)
        {
            exerciseRepository.Insert(exercise);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
