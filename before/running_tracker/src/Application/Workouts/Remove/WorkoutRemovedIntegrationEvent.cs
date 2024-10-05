using Application.Abstractions.Messaging;

namespace Application.Workouts.Remove;

public record WorkoutRemovedIntegrationEvent(Guid Id, Guid WorkoutId) : IntegrationEvent(Id);
