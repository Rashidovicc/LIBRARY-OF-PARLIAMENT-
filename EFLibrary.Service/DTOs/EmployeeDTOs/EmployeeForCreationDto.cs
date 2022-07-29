using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EFLibrary.Domain.Entities.Libraries;
using EFLibrary.Domain.Enums;
using EFLibrary.Service.Extentions.Attributes;

namespace EFLibrary.Service.DTOs.EmployeeDTOs
{
    public class EmployeeForCreationDto
    {
        [Required,MaxLength(64),UserValidation]
        public string Fullname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string UserName { get; set; }
      
        [Required]
        public EmployeeProfession Profession { get; set; }
        [MaxLength(100),Required,EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [JsonIgnore]
        public string Password { get; set; }
        
        public long LibraryId { get; set; } 
        public Library Library { get; set; }
    }
}