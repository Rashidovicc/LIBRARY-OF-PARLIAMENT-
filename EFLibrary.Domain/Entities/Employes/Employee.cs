using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Libraries;
using EFLibrary.Domain.Enums;

namespace EFLibrary.Domain.Entities.Employes
{
    public class Employee : IAuditable
    {
        public long Id { get; set; }
        [Required,MaxLength(64)]
        public string Fullname { get; set; }
        
        public string Address { get; set; }
        public string UserName { get; set; }
        
        
        public EmployeeProfession Profession { get; set; }
        [MaxLength(100),Required,EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [JsonIgnore]
        public string Password { get; set; }
        
        public long LibraryId { get; set; }
        public Library Library { get; set; }
        
        
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