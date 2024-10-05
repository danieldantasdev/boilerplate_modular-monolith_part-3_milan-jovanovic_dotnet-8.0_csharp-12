using Application.Abstractions.Messaging;

namespace Application.Workouts.GetById;

public sealed record GetWorkoutByIdQuery(Guid WorkoutId) : IQuery<WorkoutResponse>;
