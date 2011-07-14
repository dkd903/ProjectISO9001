using System.ComponentModel.DataAnnotations;
using SuperSchnell.Project.Domain;

namespace SuperSchnell.Project.Mvc.Models.CoolingProject
{
    public class EditCoolingProjectViewModel:EntityEditViewModel
    {
        public EditCoolingProjectViewModel()
        {
        }

        public EditCoolingProjectViewModel(ICoolingProject project)
            :base(project.Id,project.Version)
        {
            Description = project.Description;
            CustomerName = project.CustomerName;
            CustomerAccount = project.CustomerAccount;
            CustomerId = project.Customer == null ? (long?) null : project.Customer.Id;
        }
        [Required]
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAccount { get; set; }
        public long? CustomerId { get; set; }
    }
}