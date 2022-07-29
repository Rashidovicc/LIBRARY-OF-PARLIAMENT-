using System;
using System.ComponentModel.DataAnnotations;
using EFLibrary.Service.Extentions.Attributes;

namespace EFLibrary.Service.DTOs.UserDTOs
{
    public abstract class UserForChangePassword
    {
        [Required,UserValidation]
        public string UserName { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required,PasswordValidation]
        public string NewPassword { get; set; }
        [Required,Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}