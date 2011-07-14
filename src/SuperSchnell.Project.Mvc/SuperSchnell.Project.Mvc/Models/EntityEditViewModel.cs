using System.Web.Mvc;

namespace SuperSchnell.Project.Mvc.Models
{
    public abstract class EntityEditViewModel
    {
        protected EntityEditViewModel()
        {
            
        }

        protected EntityEditViewModel(long id, int version)
        {
            Id = id;
            Version = version;
        }
        [HiddenInput]
        public long Id { get; set; }
        [HiddenInput]
        public int Version { get; set; }
    }
}