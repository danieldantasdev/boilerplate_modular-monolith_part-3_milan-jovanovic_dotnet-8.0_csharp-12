using Application.Followers.StartFollowing;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

public class StartFollowing : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/{userId}/follow/{followedId}", async (
            Guid userId,
            Guid followedId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new StartFollowingCommand(userId, followedId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
