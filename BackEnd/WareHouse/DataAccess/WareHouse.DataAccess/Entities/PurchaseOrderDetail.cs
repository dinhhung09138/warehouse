using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Purchase order detail table
    /// </summary>
    public class PurchaseOrderDetail
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid WareHouseId { get; set; }
        public Guid GoodsId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public string Notes { get; set; }
    }
}
