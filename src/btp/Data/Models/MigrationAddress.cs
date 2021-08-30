// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigrationAddress.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the MigrationAddress type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Data.Models
{
    /// <summary>
    /// The migration address.
    /// </summary>
    public class MigrationAddress
    {
        /// <summary>
        /// Gets or sets the address id.
        /// </summary>
        public string AddressId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether default.
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address one.
        /// </summary>
        public string AddressOne { get; set; }

        /// <summary>
        /// Gets or sets the address two.
        /// </summary>
        public string AddressTwo { get; set; }

        /// <summary>
        /// Gets or sets the address three.
        /// </summary>
        public string AddressThree { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        public string ZipCode { get; set; }

    }
}
