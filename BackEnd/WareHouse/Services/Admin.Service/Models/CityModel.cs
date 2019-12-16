using System;
using System.ComponentModel.DataAnnotations;

namespace Admin.Service.Models
{
    /// <summary>
    /// City model.
    /// </summary>
    public class CityModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string CountryId { get; set; }

        public string CountryName { get; set; }

        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
