using Application.Abstractions.Messaging;

namespace Application.Workouts.Remove;

public sealed record RemoveWorkoutCommand(Guid WorkoutId) : ICommand;
