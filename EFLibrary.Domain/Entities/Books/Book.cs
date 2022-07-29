using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Enums;

namespace EFLibrary.Domain.Entities.Books
{
    public class Book : IAuditable
    {
        
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public long Quantity { get; set; }
        
        public BookCategory Category { get; set; }
        [Required,RegularExpression(@"^123456[0-9]{8}$")]
        public string Barcode { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ItemState State { get; set; }
        
        
        
        public void Create()
        {
            this.CreatedAt = DateTime.Now;
            this.State = ItemState.Created;
        }

        public void Update()
        {
            this.UpdatedAt = DateTime.Now;
            this.State = ItemState.Updated;
        }

        public void Delete()
        {
            this.UpdatedAt = DateTime.Now;
            this.State = ItemState.Deleted;
        }
        
    }
}