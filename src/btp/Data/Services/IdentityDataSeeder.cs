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
    using System.Reflection.Metadata;
    using System.Text.Json;
    using System.Threading.Tasks;

    using btp.Areas.Identity;
    using btp.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Syncfusion.Blazor.DropDowns;

    /// <summary>
    /// The identity data seeder.
    /// </summary>
    public class IdentityDataSeeder
    {
        /// <summary>
        /// The _user manager.
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        private readonly RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDataSeeder"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="roleManager">
        /// The role Manager.
        /// </param>
        public IdentityDataSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// The seed async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task SeedAsync()
        {
            string rolesFile = Path.Combine(Directory.GetCurrentDirectory(), "Initialization", "roles.json");
            string usersFile = Path.Combine(Directory.GetCurrentDirectory(), "Initialization", "users.json");
            string userRolesFile = Path.Combine(Directory.GetCurrentDirectory(), "Initialization", "userroles.json");

            if (File.Exists(rolesFile))
            {
                string jsonString = await File.ReadAllTextAsync(rolesFile);

                List<MigrationRole> roles = JsonSerializer.Deserialize<List<MigrationRole>>(jsonString);

                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        var appRole = new IdentityRole
                                                 {
                                                     Id = role.Id,
                                                     Name = role.Name,
                                                     NormalizedName = role.NormalizedName
                                                 };
                        await this.CreateRoleAsync(appRole);
                    }
                }
            }

            if (File.Exists(usersFile))
            {
                string jsonUsersString = await File.ReadAllTextAsync(usersFile);
                string jsonUserRolesString = await File.ReadAllTextAsync(userRolesFile);
                string jsonRolesString = await File.ReadAllTextAsync(rolesFile);

                List<MigrationUser> users = JsonSerializer.Deserialize<List<MigrationUser>>(jsonUsersString);
                List<MigrationUserRoles> userRoles = JsonSerializer.Deserialize<List<MigrationUserRoles>>(jsonUserRolesString);
                List<MigrationRole> roles = JsonSerializer.Deserialize<List<MigrationRole>>(jsonRolesString);

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

                        var curUserRoles = (from t in userRoles
                                                                 join r in roles on t.RoleId equals r.Id
                                                                 where t.UserId == user.Id
                                                                 select new { t.RoleId, r.Name }).ToList();

                        if (curUserRoles.Count > 0)
                        {
                            foreach (var role in curUserRoles)
                            {
                                var roleExists = await this.userManager.IsInRoleAsync(appUser, role.Name);

                                if (!roleExists)
                                {
                                    await this.userManager.AddToRoleAsync(appUser, role.Name);
                                }
                            }
                        }
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

        /// <summary>
        /// The create role async.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task CreateRoleAsync(IdentityRole role)
        {
            var exits = await this.roleManager.RoleExistsAsync(role.Name);
            if (!exits)
            {
                await this.roleManager.CreateAsync(role);
            }
        }
    }
}
