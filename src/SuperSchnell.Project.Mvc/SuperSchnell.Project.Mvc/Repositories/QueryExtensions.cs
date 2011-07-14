using NHibernate;

namespace SuperSchnell.Project.Mvc.Repositories
{
    public static class QueryExtensions
    {
        public static IQueryOver<T> LimitResultSet<T>(this IQueryOver<T> query, ResultOptions options)
        {
            return options.ForceNoPaging ? query : query.Skip(options.Page*options.PageSize).Take(options.PageSize);
        }
    }
}