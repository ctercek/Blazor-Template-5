using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace btp.Data.Models
{
    public class AspNetPhone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PhoneId { get; set; }
        [MaxLength(450)]
        public string UserId { get; set; }
        [PersonalData]
        [MaxLength(256)]
        [DisplayName("Name")]
        [Required(ErrorMessage = "A Name is required")]
        public string Name { get; set; }
        [PersonalData]
        [MaxLength(20)]
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "A Phone Number is required")]
        public string PhoneNumber { get; set; }


    }
}
