using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppUser.ViewModels
{
    public partial class ActivityLogViewModel
    {
        [Display(Name = "User EMail")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string UserEMail { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Action { get; set; } //insert, update, delete, send Emeil, etc

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Controller { get; set; } //product, empleyees, , etc

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(255, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Description { get; set; } // 

        public bool Status { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Type { get; set; }  //movile, web, Api
    }
}
