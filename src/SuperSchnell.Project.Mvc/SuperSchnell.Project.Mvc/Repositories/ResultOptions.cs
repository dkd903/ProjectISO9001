using System;

namespace SuperSchnell.Project.Mvc.Repositories
{
    public class ResultOptions
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool ForceNoPaging { get; set; }

        public ResultOptions()
        {
            Page = 0;
            PageSize = 20;
        }
    }
}