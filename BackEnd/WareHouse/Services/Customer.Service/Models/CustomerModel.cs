using System;

namespace Partner.Service.Models
{
    /// <summary>
    /// Customer model.
    /// </summary>
    public class CustomerModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? LogoFileId { get; set; }

        public string PrimaryPhone { get; set; }

        public string SecondaryPhone { get; set; }

        public string Fax { get; set; }

        public string Website { get; set; }

        public string TaxCode { get; set; }

        public bool IsCompany { get; set; }

        public DateTime? StartOn { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public Guid? CitiId { get; set; }

        public Guid? CountryId { get; set; }

        public decimal Longtitue { get; set; }

        public decimal Latitude { get; set; }

        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
