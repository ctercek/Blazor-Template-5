// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentityHostingStartup.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the IdentityHostingStartup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(btp.Areas.Identity.IdentityHostingStartup))]

namespace btp.Areas.Identity
{
    /// <summary>
    /// The identity hosting startup.
    /// </summary>
    public class IdentityHostingStartup : IHostingStartup
    {
        /// <summary>
        /// The configure.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}