// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the ApplicationDbContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Data
{
    using btp.Areas.Identity;
    using btp.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The application db context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the application users.
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        /// <summary>
        /// Gets or sets the asp net addresses.
        /// </summary>
        public DbSet<AspNetAddress> AspNetAddresses { get; set; }

        /// <summary>
        /// Gets or sets the asp net phones.
        /// </summary>
        public DbSet<AspNetPhone> AspNetPhones { get; set; }
    }
}
