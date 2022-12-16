using RM.Shared.Enums;

namespace RM.Shared.ViewModels
{
    public class AssignRoleToUserViewModel
    {
        [Required]
        public string UserId { get; set; }

        public string Role { get; set; } 
    }
}
