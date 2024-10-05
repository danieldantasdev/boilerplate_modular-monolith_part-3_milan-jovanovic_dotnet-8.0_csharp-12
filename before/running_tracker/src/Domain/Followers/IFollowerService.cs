using Domain.Users;
using SharedKernel;

namespace Domain.Followers;

public interface IFollowerService
{
    Task<Result<Follower>> StartFollowingAsync(
        User user,
        User followed,
        CancellationToken cancellationToken);
}
