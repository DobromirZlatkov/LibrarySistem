namespace DigitalLibrary.Models
{
    using DigitalLibrary.Data.Contracts;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        [Editable(false)]
        [Column(TypeName = "DateTime")]
        public DateTime DatePosted { get; set; }

        public string PostedById { get; set; }

        public virtual User PostedBy { get; set; }

        public int WorkId { get; set; }

        public virtual Work Work { get; set; }
    }
}
