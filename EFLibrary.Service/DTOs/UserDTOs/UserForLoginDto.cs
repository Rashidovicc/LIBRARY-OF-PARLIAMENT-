using System.ComponentModel.DataAnnotations;
using EFLibrary.Service.Extentions.Attributes;

namespace EFLibrary.Service.DTOs.UserDTOs
{
    public class UserForLoginDto
    {
        [Required,UserValidation]
        public string Username { get; set; }
        [Required,PasswordValidation]
        public string Password { get; set; }
    }
}