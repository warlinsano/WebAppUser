using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebAppUser.Data;

namespace WebAppUser.Models
{
    public class ActivityLog
    {
        // assign GUID to Id
        //user.Id = Guid.NewGuid().ToString();
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IdActivityLog { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Ip { get; set; }

        [Display(Name = "Mac Address")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string MacAddress { get; set; }

        [Display(Name = "Name Device")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string NameDevice { get; set; }

        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string UserId { get; set; }

        [Display(Name = "User EMail")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string UserEMail { get; set; }

        [Display(Name = "Full Name")]
        public string UserFullName { get; set; }

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

        [JsonIgnore]
        [NotMapped]
        public string Photo { get; set; }

    }
}
