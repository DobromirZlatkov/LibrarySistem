namespace DigitalLibrary.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using DigitalLibrary.Data.Contracts;

    public class Author : DeletableEntity
    {
        private ICollection<Work> works;

        public Author()
        {
            this.Works = new HashSet<Work>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Work> Works
        {
            get { return this.works; }
            set { this.works = value; }
        }
    }
}
