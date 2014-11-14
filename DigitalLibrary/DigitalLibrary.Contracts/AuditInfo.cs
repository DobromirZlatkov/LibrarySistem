namespace DigitalLibrary.Data.Contracts
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class AuditInfo : IAuditInfo
    {
        [Column(TypeName = "DateTime")]
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Specifies whether or not the CreatedOn property should be automatically set.
        /// </summary>
        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? ModifiedOn { get; set; }
    }
}
