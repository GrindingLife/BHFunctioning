using System.ComponentModel.DataAnnotations;

namespace BHFunctioning.Models
{
    public class Role
    {
        [Required]
        public string Name { get; set; }
    }

    public class EditRoleModel
    {
        
        public string Id { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role Name is required")]
        public string Name { get; set; }
        public List<string> Users { get; set; } = new();

    }

    public class UserRoleModel
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