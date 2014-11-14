namespace DigitalLibrary.Data.Contracts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class DeletableEntity : AuditInfo, IDeletableEntity
    {
        [Editable(false)]
        public bool IsDeleted { get; set; }

        [Editable(false)]
        [Column(TypeName = "DateTime")]
        public DateTime? DeletedOn { get; set; }
    }
}
