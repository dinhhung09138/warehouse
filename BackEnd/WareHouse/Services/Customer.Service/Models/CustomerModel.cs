using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Service.Models
{
    /// <summary>
    /// Customer model.
    /// </summary>
    public class CustomerModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public string LogoFileId { get; set; }

        [MaxLength(20)]
        public string PrimaryPhone { get; set; }

        [MaxLength(20)]
        public string SecondaryPhone { get; set; }

        [MaxLength(20)]
        public string Fax { get; set; }

        [MaxLength(50)]
        public string Website { get; set; }

        [MaxLength(50)]
        public string TaxCode { get; set; }

        public bool IsCompany { get; set; }

        public DateTime? StartOn { get; set; }

        public string Description { get; set; }

        [MaxLength(300)]
        public string Address { get; set; }

        public string CityId { get; set; }

        public string CityName { get; set; }

        public string CountryId { get; set; }

        public string CountryName { get; set; }

        public decimal Longtitue { get; set; }

        public decimal Latitude { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string ContactName { get; set; }

        [MaxLength(50)]
        public string ContactPhone { get; set; }

        [MaxLength(50)]
        public string ContactEmail { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(250)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
