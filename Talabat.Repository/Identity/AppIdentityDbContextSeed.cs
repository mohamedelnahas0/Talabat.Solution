using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsyc(UserManager<AppUser> userManager ) 
        {
            if(!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Mohamed Elnahas",
                    Email = "muhmedelnhas@gmail.com",
                    UserName = "Mohamedelnhas",
                    PhoneNumber = "01016928780"

                };
                await userManager.CreateAsync(User, "Password123!");

            }

        }
    }
}
