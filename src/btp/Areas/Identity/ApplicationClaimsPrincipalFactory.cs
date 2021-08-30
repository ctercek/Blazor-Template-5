// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationClaimsPrincipalFactory.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the ApplicationClaimsPrincipalFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Areas.Identity
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// The application claims principal factory.
    /// </summary>
    public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationClaimsPrincipalFactory"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="roleManager">
        /// The role manager.
        /// </param>
        /// <param name="optionsAccessor">
        /// The options accessor.
        /// </param>
        public ApplicationClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        /// <summary>
        /// The create async.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                ((ClaimsIdentity)principal.Identity)?.AddClaims(new[] {
                                                                              new Claim(ClaimTypes.GivenName, user.FirstName)
                                                                          });
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                ((ClaimsIdentity)principal.Identity)?.AddClaims(new[] {
                                                                              new Claim(ClaimTypes.Surname, user.LastName),
                                                                          });
            }

            return principal;
        }



    }
}
