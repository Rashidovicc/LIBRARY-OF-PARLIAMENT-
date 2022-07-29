using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Books;
using EFLibrary.Domain.Entities.Orders;
using EFLibrary.Domain.Enums;

namespace EFLibrary.Domain.Entities.Users
{
    public class User : IAuditable
    {
        public long Id { get; set; }
        
        [Required]
        public string FullName { get; set; }
        
        [Required]
        public string UserName { get; set; }
        [MaxLength(64),Required,EmailAddress]
        public string Email { get; set; }
        [Phone,Required,MaxLength(10)]
        public string Phone { get; set; }
        
        [DataType(DataType.Password)]
        [JsonIgnore]
        public string Password { get; set; }
        [Required]
        
        public string Address { get; set; }
        
        
        public ICollection<Order> Orders { get; set; }
       

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