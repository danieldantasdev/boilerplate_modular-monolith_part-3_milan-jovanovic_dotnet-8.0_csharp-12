using Domain.Activities;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

internal sealed class ActivityRepository(ApplicationDbContext context) : IActivityRepository
{
    public void Insert(Activity activity)
    {
        context.Activities.Add(activity);
    }
}
