namespace DigitalLibrary.Data.Contracts
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public interface IAuditInfo
    {
        [Column(TypeName = "DateTime")]
        DateTime? CreatedOn { get; set; }

        bool PreserveCreatedOn { get; set; }

        [Column(TypeName = "DateTime")]
        DateTime? ModifiedOn { get; set; }
    }
}
