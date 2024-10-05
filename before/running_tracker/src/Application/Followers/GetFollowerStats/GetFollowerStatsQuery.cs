using Application.Abstractions.Messaging;

namespace Application.Followers.GetFollowerStats;

public sealed record GetFollowerStatsQuery(Guid UserId) : IQuery<FollowerStatsResponse>;
