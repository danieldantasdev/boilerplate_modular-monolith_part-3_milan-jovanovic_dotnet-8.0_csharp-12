using Application.Abstractions.Messaging;

namespace Application.Workouts.AddExercises;

public sealed record AddExercisesCommand(Guid WorkoutId, List<ExerciseRequest> Exercises)
    : ICommand;
