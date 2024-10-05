using Application.Abstractions.Messaging;

namespace Application.Followers.GetRecentFollowers;

public sealed record GetRecentFollowersQuery(Guid UserId) : IQuery<List<FollowerResponse>>;
