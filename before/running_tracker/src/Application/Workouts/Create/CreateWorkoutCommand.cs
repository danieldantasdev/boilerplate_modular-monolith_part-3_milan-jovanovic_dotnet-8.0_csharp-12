using Application.Abstractions.Messaging;

namespace Application.Workouts.Create;

public sealed record CreateWorkoutCommand(Guid UserId, string Name) : ICommand<Guid>;
