namespace TangoSchool.ApplicationServices.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int itemPerPage, int page)
    {
        return query
            .Skip(itemPerPage * page)
            .Take(itemPerPage);
    }
}
