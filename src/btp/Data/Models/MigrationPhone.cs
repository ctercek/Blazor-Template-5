// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigrationPhone.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the MigrationPhone type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Data.Models
{
    /// <summary>
    /// The migration phone.
    /// </summary>
    public class MigrationPhone
    {
        /// <summary>
        /// Gets or sets the phone id.
        /// </summary>
        public string PhoneId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use for text messaging.
        /// </summary>
        public bool UseForTextMessaging { get; set; }
    }
}
