// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentityDataSeeder.cs" company="Company Here">
//   Copyright Company Here
// </copyright>
// <summary>
//   Defines the SeedDatabase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using btp.Areas.Identity;
    using btp.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The identity data seeder.
    /// </summary>
    public class IdentityDataSeeder
    {
        /// <summary>
        /// The _user manager.
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDataSeeder"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        public IdentityDataSeeder(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// The seed async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task SeedUsersAsync()
        {

            string pathToFile = Path.Combine(Directory.GetCurrentDirectory(), "Initialization", "users.json");
            if (File.Exists(pathToFile))
            {
                string jsonString = await File.ReadAllTextAsync(pathToFile);

                List<MigrationUser> users = JsonSerializer.Deserialize<List<MigrationUser>>(jsonString);

                if (users != null)
                {
                    foreach (MigrationUser user in users)
                    {
                        var appUser = new ApplicationUser
                                          {
                                              Id = user.Id,
                                              UserName = user.UserName,
                                              NormalizedUserName = user.NormalizedUserName,
                                              Email = user.Email,
                                              NormalizedEmail = user.NormalizedEmail,
                                              EmailConfirmed = true,
                                              FirstName = user.FirstName,
                                              LastName = user.LastName
                                          };

                        await this.CreateUserAsync(appUser, user.Password);
                    }
                }
            }
        }

        /// <summary>
        /// The create user async.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task CreateUserAsync(ApplicationUser user, string password)
        {
            var exists = await this.userManager.FindByEmailAsync(user.Email);
            if (exists == null)
            {
                await this.userManager.CreateAsync(user, password);
            }
        }


    }
}
