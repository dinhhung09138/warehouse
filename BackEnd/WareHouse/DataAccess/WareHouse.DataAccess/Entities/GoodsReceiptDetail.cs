using System;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Goods receipt detail table.
    /// </summary>
    public class GoodsReceiptDetail
    {
        public Guid Id { get; set; }
        public Guid ReceiptId { get; set; }
        public Guid WareHouseId { get; set; }
        public Guid GoodsId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public string Notes { get; set; }
    }
}
