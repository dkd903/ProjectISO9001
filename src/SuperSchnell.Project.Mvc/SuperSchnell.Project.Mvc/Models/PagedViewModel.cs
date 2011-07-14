using System;
using System.Collections.Generic;
using SuperSchnell.Project.Mvc.Repositories;

namespace SuperSchnell.Project.Mvc.Models
{
    public class PagedViewModel<TEntity>
    {
        public PagedViewModel(PagedResultSet<TEntity> resultSet,ResultOptions options)
        {
            Entities = resultSet.Page;
            Count = resultSet.Count;
            CurrentPage = options.Page;
            CurrentPageSize = options.PageSize;
            NumberOfPages = (int)Math.Ceiling((decimal)Count / CurrentPageSize);
        }

        public IList<TEntity> Entities { get; private set; }
        public int Count { get; private set; }
        public int CurrentPage { get; private set; }
        public int CurrentPageSize { get; private set; }
        public int NumberOfPages { get; private set; }
    }
}