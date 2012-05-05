using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameSchool.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name = "User ID")]
        public string User_ID { get; set; }

        [Required]
        [Display(Name = "Notendanafn")]
        public string UserName { get; set; }

        [Required]
        [Display(Name= "Fullt nafn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Kennitala")]
        public string SSN { get; set; }

        [Required]
        [Display(Name = "Heimilisfang")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Sími")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Netfang")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Lykilorðið þarf að vera að minnsta kosti {2} stafir að lengd.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Staðfesta lykilorð")]
        [Compare("Password", ErrorMessage = "Lykilorðin passa ekki. Reyndu aftur.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role_ID { get; set; }

        [Display(Name = "Tegund notanda")]
        public List<string> RoleName { get; set; }
    }
}