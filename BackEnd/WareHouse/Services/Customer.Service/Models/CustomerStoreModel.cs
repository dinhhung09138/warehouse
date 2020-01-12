using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Service.Models
{
    /// <summary>
    /// Customer store model.
    /// </summary>
    public class CustomerStoreModel
    {
        public string Id { get; set; }

        public string ClientId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string PrimaryPhone { get; set; }

        [MaxLength(20)]
        public string SecondaryPhone { get; set; }

        [MaxLength(20)]
        public string Fax { get; set; }

        [MaxLength(20)]
        public string Email { get; set; }

        public string StoreManagerId { get; set; }

        public string StoreManagerName { get; set; }

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

        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
