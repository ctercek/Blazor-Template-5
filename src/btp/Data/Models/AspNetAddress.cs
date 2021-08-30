// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspNetAddress.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the AspNetAddress type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Data.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The asp net address.
    /// </summary>
    public class AspNetAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AddressId { get; set; }
        [MaxLength(450)]
        public string UserId { get; set; }
        [DisplayName("Is Default Address")]
        public bool Default { get; set; }
        [PersonalData]
        [MaxLength(256)]
        [DisplayName("Address Name")]
        [Required]
        public string Name { get; set; }
        [PersonalData]
        [MaxLength(256)]
        [DisplayName("Address One")]
        [Required]
        public string AddressOne { get; set; }
        [PersonalData]
        [MaxLength(256)]
        [DisplayName("Address Two")]
        public string AddressTwo { get; set; }
        [PersonalData]
        [MaxLength(256)]
        [DisplayName("Address Three")]
        public string AddressThree { get; set; }
        [PersonalData]
        [MaxLength(256)]
        [DisplayName("City")]
        [Required]
        public string City { get; set; }
        [PersonalData]
        [MaxLength(256)]
        [DisplayName("State")]
        [Required]
        public string State { get; set; }
        [PersonalData]
        [MaxLength(256)]
        [DisplayName("Zip Code")]
        [Required]
        public string ZipCode { get; set; }


    }
}
