// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressService.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the AddressService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using btp.Areas.Identity;
    using btp.Data.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The address service.
    /// </summary>
    public class AddressService
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
        /// Initializes a new instance of the <see cref="AddressService"/> class.
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
        public AddressService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        /// <summary>
        /// The get addresses async.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<List<AspNetAddress>> GetAddressesAsync(string userName)
        {
            var results = (from t in this.context.AspNetAddresses
                join r in this.context.Users on t.UserId equals r.Id
                where r.UserName == userName
                select t).ToList();

            return Task.FromResult(results);
        }

        /// <summary>
        /// The add address.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        public void AddAddress(AspNetAddress address)
        {
            if (address.AddressId == null)
            {
                address.AddressId = Guid.NewGuid().ToString();
            }

            if (!this.context.AspNetAddresses.Any(c => c.AddressId == address.AddressId))
            {
                this.context.AspNetAddresses.Add(address);
                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// The add address async.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        public void AddAddressAsync(AspNetAddress address)
        {
            if (address.AddressId == null)
            {
                address.AddressId = Guid.NewGuid().ToString();
            }

            if (!this.context.AspNetAddresses.Any(c => c.AddressId == address.AddressId))
            {
                this.context.AspNetAddresses.Add(address);
                this.context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// The update address.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        public void UpdateAddress(AspNetAddress address)
        {
            var addr = this.context.AspNetAddresses.FirstOrDefault(s => s.AddressId.Equals(address.AddressId));

            if (addr != null)
            {
                var st = this.context.Entry((object)addr).State;

                addr.AddressOne = address.AddressOne;
                addr.AddressTwo = address.AddressTwo;
                addr.AddressThree = address.AddressThree;
                addr.City = address.City;
                addr.State = address.State;
                addr.ZipCode = address.ZipCode;
                addr.Name = address.Name;
                addr.Default = address.Default;

                this.context.AspNetAddresses.Update(addr);

                this.context.SaveChanges();
            }

        }

        /// <summary>
        /// The remove address.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        public void RemoveAddress(AspNetAddress address)
        {
            var addr = this.context.AspNetAddresses.FirstOrDefault(s => s.AddressId.Equals(address.AddressId));
            this.context.AspNetAddresses.Remove(addr);
            this.context.SaveChanges();

        }


    }
}
