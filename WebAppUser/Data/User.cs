using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAppUser.Data
{
    public class User : IdentityUser
    {
        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }


        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        public bool Estado { get; set; }

        [JsonIgnore]
        public byte[] Photo { get; set; }

        [JsonIgnore]
        public string PhotoPath { get; set; }

        [JsonIgnore]
        [NotMapped]
        public string PhotoBase64
        {
            get
            {
                return (Photo == null ? string.Empty : String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(Photo)));
            }
        }


        [NotMapped]
        public string[,] todosRoles { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(PhotoPath)
           ? null : $"http://warlinsano.somee.com{PhotoPath.Substring(1)}";

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Full Name")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }
}
