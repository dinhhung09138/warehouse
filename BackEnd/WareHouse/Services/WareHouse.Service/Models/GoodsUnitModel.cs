using System;
using System.Collections.Generic;
using System.Text;

namespace WareHouse.Service.Models
{
    /// <summary>
    /// Goods unit model.
    /// </summary>
    public class GoodsUnitModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
