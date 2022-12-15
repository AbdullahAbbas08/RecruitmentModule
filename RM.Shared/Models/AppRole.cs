namespace RM.Shared
{
    public class AppRole: IdentityRole
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
} 