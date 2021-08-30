// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the ApplicationUser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Areas.Identity
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The application user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        //// [PersonalData]
        //// public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [PersonalData]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [PersonalData]
        public string LastName { get; set; }

    }
}
