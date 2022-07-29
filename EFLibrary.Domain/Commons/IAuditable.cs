using System;
using EFLibrary.Domain.Enums;

namespace EFLibrary.Domain.Commons
{
    public interface IAuditable
    {
        
        long Id { get; set; } 
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        ItemState State { get; set; }

        public void Create();
        public void Update();
        public void Delete();
    }
}