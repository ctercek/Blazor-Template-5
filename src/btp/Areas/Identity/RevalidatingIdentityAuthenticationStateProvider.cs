// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RevalidatingIdentityAuthenticationStateProvider.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   The revalidating identity authentication state provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Areas.Identity
{
    using System;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Server;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// The revalidating identity authentication state provider.
    /// </summary>
    /// <typeparam name="TUser">
    /// </typeparam>
    public class RevalidatingIdentityAuthenticationStateProvider<TUser>
        : RevalidatingServerAuthenticationStateProvider where TUser : class
    {
        /// <summary>
        /// The _scope factory.
        /// </summary>
        private readonly IServiceScopeFactory scopeFactory;

        /// <summary>
        /// The options.
        /// </summary>
        private readonly IdentityOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="RevalidatingIdentityAuthenticationStateProvider{TUser}"/> class.
        /// </summary>
        /// <param name="loggerFactory">
        /// The logger factory.
        /// </param>
        /// <param name="scopeFactory">
        /// The scope factory.
        /// </param>
        /// <param name="optionsAccessor">
        /// The options accessor.
        /// </param>
        public RevalidatingIdentityAuthenticationStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory scopeFactory,
            IOptions<IdentityOptions> optionsAccessor)
            : base(loggerFactory)
        {
            this.scopeFactory = scopeFactory;
            this.options = optionsAccessor.Value;
        }

        /// <summary>
        /// The revalidation interval.
        /// </summary>
        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

        /// <summary>
        /// The validate authentication state async.
        /// </summary>
        /// <param name="authenticationState">
        /// The authentication state.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected override async Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            // Get the user manager from a new scope to ensure it fetches fresh data
            var scope = this.scopeFactory.CreateScope();
            try
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TUser>>();
                return await ValidateSecurityStampAsync(userManager, authenticationState.User);
            }
            finally
            {
                if (scope is IAsyncDisposable asyncDisposable)
                {
                    await asyncDisposable.DisposeAsync();
                }
                else
                {
                    scope.Dispose();
                }
            }
        }

        /// <summary>
        /// The validate security stamp async.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="principal">
        /// The principal.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<bool> ValidateSecurityStampAsync(UserManager<TUser> userManager, ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            if (user == null)
            {
                return false;
            }
            else if (!userManager.SupportsUserSecurityStamp)
            {
                return true;
            }
            else
            {
                var principalStamp = principal.FindFirstValue(this.options.ClaimsIdentity.SecurityStampClaimType);
                var userStamp = await userManager.GetSecurityStampAsync(user);
                return principalStamp == userStamp;
            }
        }
    }
}
