using TangoSchool.DataAccess.DatabaseContexts;
using TangoSchool.DataAccess.Entities;
using TangoSchool.DataAccess.Repositories.Interfaces;

namespace TangoSchool.DataAccess.Repositories;

internal class SubscriptionsTemplatesRepository : BaseRepository<SubscriptionTemplate>,
    ISubscriptionsTemplatesRepository
{
    public SubscriptionsTemplatesRepository(TangoSchoolDbContext context) : base(context)
    {
    }
}
