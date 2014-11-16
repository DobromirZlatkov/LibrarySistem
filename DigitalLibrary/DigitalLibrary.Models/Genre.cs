namespace DigitalLibrary.Models
{
    using DigitalLibrary.Data.Contracts;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre : DeletableEntity
    {
        private ICollection<Work> works;

        public Genre()
        {
            this.works = new HashSet<Work>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string GenreName { get; set; }

        public virtual ICollection<Work> Works
        {
            get { return this.works; }
            set { this.works = value; }
        }
    }
}
