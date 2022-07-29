using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFLibrary.Service.DTOs.LibraryDTOs
{
    public class LibraryForCreationDto
    {
        [Required, MaxLength(64)]
        public string Name { get; set; }
        [Required, MaxLength(64)]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        
        

    }
}