namespace DigitalLibrary.Models
{
    using DigitalLibrary.Data.Contracts;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
