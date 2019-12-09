using System;
using System.ComponentModel.DataAnnotations;

namespace WareHouse.Service.Models
{
    /// <summary>
    /// Goods category model.
    /// </summary>
    public class GoodsCategoryModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
