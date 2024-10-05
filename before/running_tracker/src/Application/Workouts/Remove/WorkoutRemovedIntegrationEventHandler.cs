using MediatR;

namespace Application.Workouts.Remove;

internal sealed class WorkoutRemovedIntegrationEventHandler : INotificationHandler<WorkoutRemovedIntegrationEvent>
{
    public async Task Handle(WorkoutRemovedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
    }
}
