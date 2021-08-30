// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentityExtensions.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the IdentityExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Areas.Identity
{
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>
    /// The identity extensions.
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// The first name.
        /// </summary>
        /// <param name="identity">
        /// The identity.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FirstName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.GivenName);
            //// Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        /// <summary>
        /// The last name.
        /// </summary>
        /// <param name="identity">
        /// The identity.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string LastName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Surname);
            //// Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
