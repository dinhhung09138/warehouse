using System;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Purchase order fee table.
    /// </summary>
    public class PurchaseOrderFee
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public string Name { get; set; }
        public decimal Fee { get; set; }
        public string Notes { get; set; }
    }
}
