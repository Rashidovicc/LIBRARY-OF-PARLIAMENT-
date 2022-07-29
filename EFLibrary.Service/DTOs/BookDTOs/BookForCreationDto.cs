using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EFLibrary.Domain.Enums;
using EFLibrary.Service.Extentions.Attributes;

namespace EFLibrary.Service.DTOs.BookDTOs
{
    public class BookForCreationDto
    {
        [MaxLength(50),UserValidation,Required]
        public string Title { get; set; }
        [StringLength(50),UserValidation,Required]
        public string Author { get; set; }
        
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public long Quantity { get; set; }
        
        public BookCategory Category { get; set; }
        [Required,RegularExpression(@"^123456[0-9]{8}$")]
        public string Barcode { get; set; }
    }
}