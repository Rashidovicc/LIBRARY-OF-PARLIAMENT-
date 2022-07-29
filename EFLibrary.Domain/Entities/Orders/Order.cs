using System;
using System.ComponentModel.DataAnnotations.Schema;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Books;
using EFLibrary.Domain.Entities.Employes;
using EFLibrary.Domain.Entities.Libraries;
using EFLibrary.Domain.Entities.Users;
using EFLibrary.Domain.Enums;

namespace EFLibrary.Domain.Entities.Orders
{
    public class Order : IAuditable
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        
        public long BookId { get; set; }
        public Book Book { get; set; }
        
        public long LibraryId { get; set; }
        
        public Library Library { get; set; }

        public long EmployeeId { get; set; } = 1;
        public Employee Employee { get; set; }
        public int Quantity { get; set; }
        
        [NotMapped]
        public decimal Total { get => (Book.Price + ShippingFee) * Quantity;}

       

        public decimal ShippingFee { get; set; }
        public string ShippingAddress { get; set; }
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
            this.CreatedAt = DateTime.Now;
            this.State = ItemState.Updated;
        }

        public void Delete()
        {
            this.UpdatedAt = DateTime.Now;
            this.State = ItemState.Deleted;
        }
    }
}