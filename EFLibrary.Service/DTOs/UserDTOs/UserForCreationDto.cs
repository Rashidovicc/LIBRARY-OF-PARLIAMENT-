using System;
using System.ComponentModel.DataAnnotations;
using EFLibrary.Service.Extentions.Attributes;

namespace EFLibrary.Service.DTOs.UserDTOs
{
    public class UserForCreationDto
    {
        [Required, MaxLength(50),UserValidation]
        public string FullName { get; set; }
        [Required, MaxLength(50), UserValidation]
        public  string UserName { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [Phone, Required]
        public string Phone { get; set; }
        [MinLength(6),MaxLength(50), Required]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; }
    }
}