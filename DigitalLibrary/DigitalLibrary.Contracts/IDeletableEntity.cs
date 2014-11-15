namespace DigitalLibrary.Data.Contracts
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        [Column(TypeName = "DateTime")]
        DateTime? DeletedOn { get; set; }
    }
}
