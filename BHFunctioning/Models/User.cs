using System.ComponentModel.DataAnnotations;

namespace BHFunctioning.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
    public class EditUserModel
    {

        public string Id { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "User Name is required")]
        public string Name { get; set; }
        public List<string> Users { get; set; } = new();

    }
    public class RolesModel
    {
        //User id
        public string Id { get; set; }
        //User Name
        public string Name { get; set; }
        //Role Name
        public string rName { get; set; }

        public string rId { get; set; }
        public bool IsSelected { get; set; }
    }
}