using Domain.Workouts;

namespace Application.Workouts.GetById;

public sealed record ExerciseResponse
{
    public Guid ExerciseId { get; init; }

    public ExerciseType ExerciseType { get; init; }

    public TargetType TargetType { get; init; }

    public decimal? Distance { get; init; }

    public TimeSpan? Duration { get; init; }
}
