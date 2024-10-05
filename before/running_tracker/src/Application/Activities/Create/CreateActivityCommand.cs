using Application.Abstractions.Messaging;

namespace Application.Activities.Create;

public sealed record CreateActivityCommand(
    Guid UserId,
    Guid WorkoutId,
    decimal DistanceInMeters,
    int DurationInSeconds) : ICommand<Guid>;
