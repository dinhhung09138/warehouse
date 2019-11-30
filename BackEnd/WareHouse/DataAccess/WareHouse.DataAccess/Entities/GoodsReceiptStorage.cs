using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Goods receipt storage table.
    /// </summary>
    public class GoodsReceiptStorage
    {
        public Guid Id { get; set; }
        public Guid ReceiptId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid WarehouseId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalCost { get; set; }
        public string Notes { get; set; }
    }
}
