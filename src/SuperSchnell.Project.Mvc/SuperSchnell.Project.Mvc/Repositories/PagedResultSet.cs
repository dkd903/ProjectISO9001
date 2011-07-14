using System.Collections.Generic;

namespace SuperSchnell.Project.Mvc.Repositories
{
    public class PagedResultSet<TEntity>
    {
        public PagedResultSet(IList<TEntity> page, int count)
        {
            Page = page;
            Count = count;
        }

        public IList<TEntity> Page { get; private set; }
        public int Count { get; private set; }
    }
}