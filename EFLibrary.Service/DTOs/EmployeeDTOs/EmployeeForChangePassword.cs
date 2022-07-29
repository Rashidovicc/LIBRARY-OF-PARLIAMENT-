using System.ComponentModel.DataAnnotations;
using EFLibrary.Service.Extentions.Attributes;

namespace EFLibrary.Service.DTOs.EmployeeDTOs
{
    public abstract class EmployeeForChangePassword
    {
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}