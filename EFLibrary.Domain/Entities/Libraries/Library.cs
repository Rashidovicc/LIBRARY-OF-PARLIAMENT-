using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Employes;
using EFLibrary.Domain.Enums;

namespace EFLibrary.Domain.Entities.Libraries
{
    public class Library : IAuditable
    {
        
        public long Id { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; }
        
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        
        public ICollection<Employee> Employees { get; set; }

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