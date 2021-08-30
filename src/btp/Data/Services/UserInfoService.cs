// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserInfoService.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the UserInfoService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Data.Services
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using btp.Areas.Identity;
    using btp.Data;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The user info service.
    /// </summary>
    public class UserInfoService
    {
        /// <summary>
        /// The context.
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// The http context accessor.
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfoService"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="httpContextAccessor">
        /// The http context accessor.
        /// </param>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        public UserInfoService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        /// <summary>
        /// The get users async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<IdentityUser[]> GetUsersAsync()
        {

            var results = this.context.Users.ToArray();

            return Task.FromResult(results);
        }

        /// <summary>
        /// The get users async.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<IdentityUser[]> GetUsersAsync(string userName)
        {
            var results = (from t in this.context.Users
                           where t.UserName == userName
                           select t).ToArray();

            return Task.FromResult(results);
        }

        /// <summary>
        /// The get current user.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetCurrentUser()
        {
            return this.httpContextAccessor.HttpContext.User.Identity.Name;
        }

        public string GetCurrentUserId()
        {
            return this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }







    }
}
