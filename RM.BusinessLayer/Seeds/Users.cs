using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.BusinessLayer.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminUser(UserManager<AppUser> UserManager, RoleManager<AppRole> roleManager)
        {
            try
            {
                var DefaultUser = new AppUser
                {
                    FirstName = "Firstname",
                    LastName = "Lastname",
                    UserName = "admin250",
                    Email = "admin250@gmail.com",
                    PhoneNumber = "01287868525"
                };

                var user = await UserManager.FindByNameAsync(DefaultUser.UserName);
                if (user == null)
                {
                    await UserManager.CreateAsync(DefaultUser, "Aa123###");
                    await UserManager.AddToRolesAsync(DefaultUser, new List<string>
                {
                    Role.Admin
                });
                }
            }
            catch (Exception ex)
            {

            }
          
            

        }
    }
}
