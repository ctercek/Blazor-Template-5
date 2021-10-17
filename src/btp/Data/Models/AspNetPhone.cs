// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspNetPhone.cs" company="companyname">
//   Copyright 2021
// </copyright>
// <summary>
//   The asp net phone.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace btp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The asp net phone.
    /// </summary>
    public class AspNetPhone
    {
        /// <summary>
        /// Gets or sets the phone id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PhoneId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [MaxLength(450)]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [PersonalData]
        [MaxLength(256)]
        [DisplayName("Name")]
        [Required(ErrorMessage = "A Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [PersonalData]
        [MaxLength(20)]
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "A Phone Number is required")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use for text messaging.
        /// </summary>
        [PersonalData]
        [DisplayName("Use for Text Messaging")]
        public bool UseForTextMessaging { get; set; }
    }
}
