using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EFLibrary.Domain.Entities.Books;

namespace EFLibrary.Service.DTOs.OrderDTOs
{
    public class OrderForCreationDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int LibraryId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal ShippingFee { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
        
        
        [Required]
        public string ShipAddress { get; set; }
    }
}