using TangoSchool.DataAccess.DatabaseContexts;
using TangoSchool.DataAccess.Entities;
using TangoSchool.DataAccess.Repositories.Interfaces;

namespace TangoSchool.DataAccess.Repositories;

internal class GroupsRepository : BaseRepository<Group>, IGroupsRepository
{
    public GroupsRepository(TangoSchoolDbContext tangoSchoolDbContext) : base(tangoSchoolDbContext)
    {
    }
}
