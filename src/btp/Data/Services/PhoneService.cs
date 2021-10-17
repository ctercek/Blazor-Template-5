// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhoneService.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the PhoneService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using btp.Areas.Identity;
    using btp.Data;
    using btp.Data.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The phone service.
    /// </summary>
    public class PhoneService
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// The _http context accessor.
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// The _user manager.
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneService"/> class.
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
        public PhoneService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        /// <summary>
        /// The get phones async.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<List<AspNetPhone>> GetPhonesAsync(string userName)
        {
            var results = (from t in this.context.AspNetPhones
                join r in this.context.Users on t.UserId equals r.Id
                where r.UserName == userName
                select t).ToList();

            return Task.FromResult(results);
        }

        /// <summary>
        /// The add phone.
        /// </summary>
        /// <param name="phone">
        /// The phone.
        /// </param>
        public void AddPhone(AspNetPhone phone)
        {
            if (phone.PhoneId == null)
            {
                phone.PhoneId = Guid.NewGuid().ToString();
            }

            if (!this.context.AspNetPhones.Any(c => c.PhoneId == phone.PhoneId))
            {
                this.context.AspNetPhones.Add(phone);
                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// The add phone async.
        /// </summary>
        /// <param name="phone">
        /// The phone.
        /// </param>
        public void AddPhoneAsync(AspNetPhone phone)
        {
            if (phone.PhoneId == null)
            {
                phone.PhoneId = Guid.NewGuid().ToString();
            }

            if (!this.context.AspNetPhones.Any(c => c.PhoneId == phone.PhoneId))
            {
                this.context.AspNetPhones.Add(phone);
                this.context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// The update phone.
        /// </summary>
        /// <param name="phone">
        /// The phone.
        /// </param>
        public void UpdatePhone(AspNetPhone phone)
        {
            var ph = this.context.AspNetPhones.FirstOrDefault(s => s.PhoneId.Equals(phone.PhoneId));

            ph.Name = phone.Name;
            ph.PhoneNumber = phone.PhoneNumber;
            ph.UseForTextMessaging = phone.UseForTextMessaging;
            this.context.SaveChanges();
        }

        /// <summary>
        /// The remove phone.
        /// </summary>
        /// <param name="phone">
        /// The phone.
        /// </param>
        public void RemovePhone(AspNetPhone phone)
        {
            var ph = this.context.AspNetPhones.FirstOrDefault(s => s.PhoneId.Equals(phone.PhoneId));
            this.context.AspNetPhones.Remove(ph);
            this.context.SaveChanges();
        }


    }
}
