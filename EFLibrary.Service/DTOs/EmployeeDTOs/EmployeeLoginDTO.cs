using System.ComponentModel.DataAnnotations;
using EFLibrary.Service.Extentions.Attributes;

namespace EFLibrary.Service.DTOs.EmployeeDTOs
{
    public class EmployeeLoginDto
    {
        [Required(ErrorMessage = "Field can't be empty")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string ConfirmPassword { get; set; }
    }
}