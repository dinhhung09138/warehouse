using System;
using System.ComponentModel.DataAnnotations;

namespace Admin.Service.Models
{
    /// <summary>
    /// Country model.
    /// </summary>
    public class CountryModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
