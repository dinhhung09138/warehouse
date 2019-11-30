using System;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Purchase order delivery location table
    /// </summary>
    public class PurchaseOrderDeliveryLocation
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid CustomerStoreId { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public decimal TotalCost { get; set; }
        public string Notes { get; set; }
    }
}
