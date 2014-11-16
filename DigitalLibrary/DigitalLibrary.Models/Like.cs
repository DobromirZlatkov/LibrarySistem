namespace DigitalLibrary.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Like
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
