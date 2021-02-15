using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Projekt.Data.CustomValidation;

namespace Projekt.Data
{
    public class BlogUser : IdentityUser, IValidatableObject
    {
        public BlogUser() : base()
        {

        }

        [RegularExpression(@"([0-9]{11})", ErrorMessage = "Wrong PESEL format")]
        [StringLength(11)]
        [Required]
        public string pesel { get; set; }

        [CustomBirthDate(ErrorMessage = "Birth date must be earlier than Today's Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime dateOfBirth { get; set; }

        //public ICollection<Post> posts { get; set; } = new List<Post>();


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var date = dateOfBirth.ToShortDateString();
            Console.WriteLine(date);
            if (!(date[3] == pesel[4] && date[4] == pesel[5] && date[0] == pesel[2] && date[1] == pesel[3] && date[8] == pesel[0] && date[9] == pesel[1]))
            {
                yield return new ValidationResult("Pesel doesn't match date of birth");
            }
        }

    }
}
