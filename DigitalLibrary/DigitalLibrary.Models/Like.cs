namespace DigitalLibrary.Models
{
    using DigitalLibrary.Data.Contracts;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Like : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsPositive { get; set; }

        public string LikedById { get; set; }

        public virtual User LikedBy { get; set; }

        public int WorkId { get; set; }

        public virtual Work Work { get; set; }
    }
}
