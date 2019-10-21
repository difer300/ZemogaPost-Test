using System;
using System.ComponentModel.DataAnnotations;
using Zemoga.Models.Interfaces;

namespace Zemoga.Models
{
    public abstract class Entity : ITimestamped
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Entity()
        {
            this.CreatedAt = this.ModifiedAt = DateTime.UtcNow;
        }
    }
}
