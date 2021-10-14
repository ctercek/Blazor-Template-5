// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupIdentityDataSeeder.cs" company="CompanyName">
//   Copyright Here
// </copyright>
// <summary>
//   Defines the SetupIdentityDataSeeder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Data
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using btp.Data.Services;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The setup identity data seeder.
    /// </summary>
    public class SetupIdentityDataSeeder : IHostedService
    {
        /// <summary>
        /// The _service provider.
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupIdentityDataSeeder"/> class.
        /// </summary>
        /// <param name="serviceProvider">
        /// The service provider.
        /// </param>
        public SetupIdentityDataSeeder(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// The start async.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = this.serviceProvider.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<IdentityDataSeeder>();

                await seeder.SeedAsync();
            }
        }

        /// <summary>
        /// The stop async.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}
