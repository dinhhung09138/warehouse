using System;

namespace WareHouse.Service.Models
{
    /// <summary>
    /// Goods category model.
    /// </summary>
    public class GoodsCategoryModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
