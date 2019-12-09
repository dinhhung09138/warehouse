using System;
using System.ComponentModel.DataAnnotations;

namespace WareHouse.Service.Models
{
    /// <summary>
    /// Goods model.
    /// </summary>
    public class GoodsModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Brand { get; set; }

        [MaxLength(200)]
        public string Color { get; set; }

        [MaxLength(200)]
        public string Size { get; set; }

        public string FileId { get; set; }

        public string FileUrl { get; set; }

        public string FileName { get; set; }

        public string Description { get; set; }

        [Required]
        public string UnitId { get; set; }

        public string UnitName { get; set; }

        [Required]
        public string GoodsCategoryId { get; set; }

        public string GoodsCategoryName { get; set; }

        [Required]
        public bool IsActive { get; set; }
        
        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
