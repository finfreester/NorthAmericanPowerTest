using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NorthAmericanPower.Domain
{
    public sealed class UserAccount
    {

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Name is Required.")]
        public string Name { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Address is Required.")]
        public string Address { get; set; }

        [Display(Name = "Postal Code")]
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression("(^\\d{5}(-\\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$)", ErrorMessage = "Zip code is invalid.")] // US or Canada
        [Required(ErrorMessage = "Zip Code is Required.")]
        public string Postal { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        public string newProp { get; set; }

        public int AnotherProp { get; set; }
    }
}
