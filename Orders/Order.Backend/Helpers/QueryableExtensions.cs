using Orders.Shared.DTOs;

namespace Order.Backend.Helpers;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO pagination)
    {
        return queryable.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);
    }
}