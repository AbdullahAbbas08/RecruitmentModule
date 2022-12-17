using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.BusinessLayer.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedRole(RoleManager<AppRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new AppRole{ Name = Role.Admin });
                await roleManager.CreateAsync(new AppRole { Name = Role.Applicant });
            }
        }
    }
}
