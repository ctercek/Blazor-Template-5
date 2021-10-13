// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultIdentity.cs" company="CompanyName">
//
// </copyright>
// <summary>
//   Defines the DefaultIdentity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;

    using btp.Areas.Identity;
    using btp.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <summary>
    /// The default identity.
    /// </summary>
    public class DefaultIdentity
    {
        /// <summary>
        /// The _migration builder.
        /// </summary>
        private readonly MigrationBuilder migrationBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultIdentity"/> class.
        /// </summary>
        /// <param name="migrationBuilder">
        /// The migration builder.
        /// </param>
        public DefaultIdentity(MigrationBuilder migrationBuilder)
        {
            this.migrationBuilder = migrationBuilder;
        }

        /// <summary>
        /// The create users.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<Pending>")]
        public void CreateUsers()
        {
            string pathToFile = Path.Combine(Directory.GetCurrentDirectory(), "Initialization" ,"users.json");
            if (File.Exists(pathToFile))
            {
                List<MigrationUser> users = this.GetUsers(pathToFile);
                var hasher = new PasswordHasher<ApplicationUser>();

                foreach (MigrationUser user in users)
                {

                    string password = hasher.HashPassword(null, user.Password);

                    this.migrationBuilder.InsertData(
                        table: "AspNetUsers",
                        columns:
                            new[]
                                     {
                                        "Id",
                                        "UserName",
                                        "NormalizedUserName",
                                        "Email",
                                        "NormalizedEmail",
                                        "PasswordHash",
                                        "AccessFailedCount",
                                        "EmailConfirmed",
                                        "PhoneNumberConfirmed",
                                        "TwoFactorEnabled",
                                        "LockoutEnabled",
                                        "SecurityStamp",
                                        "ConcurrencyStamp",
                                        "FirstName",
                                        "LastName"
                                    },
                        values: new object[,]
                        {
                            {
                                user.Id,
                                user.UserName,
                                user.NormalizedUserName,
                                user.Email,
                                user.NormalizedEmail,
                                password,
                                user.AccessFailedCount,
                                user.EmailConfirmed,
                                user.PhoneNumberConfirmed,
                                user.TwoFactorEnabled,
                                user.LockoutEnabled,
                                user.SecurityStamp,
                                user.ConcurrencyStamp,
                                user.FirstName,
                                user.LastName
                            }
                        });
                }




            }
        }

        /// <summary>
        /// The create roles.
        /// </summary>
        public void CreateRoles()
        {
            string pathToFile = Path.Combine(Directory.GetCurrentDirectory(), "Initialization", "roles.json");
            if (File.Exists(pathToFile))
            {
                List<MigrationRole> roles = this.GetRoles(pathToFile);

                foreach (var role in roles)
                {
                    this.migrationBuilder.InsertData(
                        table: "AspNetRoles",
                        columns: new[]
                        {
                            "Id",
                            "Name",
                            "NormalizedName",
                            "ConcurrencyStamp"
                        },
                        values: new object[,]
                        {
                            { role.Id, role.Name, role.NormalizedName, role.ConcurrencyStamp }
                        });
                }

            }
        }

        /// <summary>
        /// The create user to roles.
        /// </summary>
        public void CreateUserToRoles()
        {
#pragma warning disable SecurityIntelliSenseCS // MS Security rules violation
            string pathToFile = Path.Combine(Directory.GetCurrentDirectory(), "Initialization", "userroles.json");
#pragma warning restore SecurityIntelliSenseCS // MS Security rules violation
            if (File.Exists(pathToFile))
            {
                List<MigrationUserRoles> roles = this.GetUserRoles(pathToFile);

                foreach (var role in roles)
                {
                    this.migrationBuilder.InsertData(
                        table: "AspNetUserRoles",
                        columns: new[] {"UserId", "RoleId"},
                        values: new object[,]
                                    {
                                        {role.UserId, role.RoleId}
                                    });
                }
            }
        }

        /// <summary>
        /// The create address.
        /// </summary>
        public void CreateAddress()
        {

#pragma warning disable SecurityIntelliSenseCS // MS Security rules violation
            string pathToFile = Path.Combine(Directory.GetCurrentDirectory(), "Initialization", "address.json");
#pragma warning restore SecurityIntelliSenseCS // MS Security rules violation
            if (File.Exists(pathToFile))
            {
                List<MigrationAddress> addresses = this.GetAddresses(pathToFile);

                foreach (var address in addresses)
                {
                    this.migrationBuilder.InsertData(
                        table: "AspNetAddresses",
                        columns: new[] { "AddressId",
                                               "UserId",
                                               "Default",
                                               "Name",
                                               "AddressOne",
                                               "AddressTwo",
                                               "AddressThree",
                                               "City",
                                               "State",
                                               "ZipCode"
                                           },
                        values: new object[,]
                                    {
                                        {
                                            address.AddressId,
                                            address.UserId,
                                            address.Default,
                                            address.Name,
                                            address.AddressOne,
                                            address.AddressTwo,
                                            address.AddressThree,
                                            address.City,
                                            address.State,
                                            address.ZipCode
                                        }
                                    });
                }
            }
        }

        /// <summary>
        /// The create phone.
        /// </summary>
        public void CreatePhone()
        {
            string pathToFile = Path.Combine(Directory.GetCurrentDirectory(), "Initialization", "phones.json");
#pragma warning restore SecurityIntelliSenseCS // MS Security rules violation
            if (File.Exists(pathToFile))
            {
                List<MigrationPhone> phoneNumbers = this.GetPhoneNumbers(pathToFile);

                foreach (var phone in phoneNumbers)
                {
                    this.migrationBuilder.InsertData(
                        table: "AspNetPhones",
                        columns: new[] { "PhoneId",
                                               "UserId",
                                               "Name",
                                               "PhoneNumber"
                                           },
                        values: new object[,]
                                    {
                                        { phone.PhoneId, phone.UserId, phone.Name, phone.PhoneNumber}
                                    });
                }
            }
        }

        /// <summary>
        /// The get users.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private List<MigrationUser> GetUsers(string path)
        {
            string jsonString = File.ReadAllText(path);

            List<MigrationUser> users = JsonSerializer.Deserialize<List<MigrationUser>>(jsonString);

            return users;
        }

        /// <summary>
        /// The get roles.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private List<MigrationRole> GetRoles(string path)
        {
            string jsonString = File.ReadAllText(path);

            List<MigrationRole> roles = JsonSerializer.Deserialize<List<MigrationRole>>(jsonString);

            return roles;
        }

        /// <summary>
        /// The get user roles.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private List<MigrationUserRoles> GetUserRoles(string path)
        {
            string jsonString = File.ReadAllText(path);
            List<MigrationUserRoles> userRoles = JsonSerializer.Deserialize<List<MigrationUserRoles>>(jsonString);
            return userRoles;
        }

        /// <summary>
        /// The get addresses.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private List<MigrationAddress> GetAddresses(string path)
        {
            string jsonString = File.ReadAllText(path);
            List<MigrationAddress> addresses = JsonSerializer.Deserialize<List<MigrationAddress>>(jsonString);
            return addresses;
        }

        /// <summary>
        /// The get phone numbers.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private List<MigrationPhone> GetPhoneNumbers(string path)
        {
            string jsonString = File.ReadAllText(path);
            List<MigrationPhone> phoneNumbers = JsonSerializer.Deserialize<List<MigrationPhone>>(jsonString);
            return phoneNumbers;
        }
    }

}
