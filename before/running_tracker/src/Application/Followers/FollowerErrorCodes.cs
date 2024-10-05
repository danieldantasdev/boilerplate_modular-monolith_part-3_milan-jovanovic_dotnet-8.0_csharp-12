namespace Application.Followers;

public static class FollowerErrorCodes
{
    public static class StartFollowing
    {
        public const string MissingUserId = nameof(MissingUserId);
        public const string MissingFollowedId = nameof(MissingFollowedId);
    }
}
