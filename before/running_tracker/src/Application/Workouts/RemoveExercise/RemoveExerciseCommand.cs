using Application.Abstractions.Messaging;

namespace Application.Workouts.RemoveExercise;

public sealed record RemoveExerciseCommand(Guid WorkoutId, Guid ExerciseId) : ICommand;
