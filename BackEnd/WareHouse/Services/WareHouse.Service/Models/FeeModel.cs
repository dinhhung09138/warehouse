using System;
using System.ComponentModel.DataAnnotations;

namespace WareHouse.Service.Models
{
    /// <summary>
    /// Fee model.
    /// </summary>
    public class FeeModel
    {
        public string Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public decimal Value { get; set; }

        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
