using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameSchool.Models.ViewModels
{
    public class ProfileManagementView
    {
        [Display(Name = "User ID")]
        public string User_ID { get; set; }

       // [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Lykilorðið þarf að vera að minnsta kosti {2} stafir að lengd.", MinimumLength = 6)]
        [Display(Name = "Nýtt Lykilorð")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Staðfesta Lykilorð")]
        [Compare("Password", ErrorMessage = "Lykilorðin passa ekki. Reyndu aftur.")]
        public string ConfirmPassword { get; set; }

       /* [DataType(DataType.ImageUrl)]*/
        [Display(Name = "Mynd")]
        public string ImageSource { get; set; }

       // [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Netfang")]
        public string Email { get; set; }
    }

}