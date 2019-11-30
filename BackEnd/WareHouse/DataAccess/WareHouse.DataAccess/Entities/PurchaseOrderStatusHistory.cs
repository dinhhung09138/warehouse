using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Purchase order status history table
    /// </summary>
    public class PurchaseOrderStatusHistory
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid UserId { get; set; }
        public bool IsEmployee { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
    }
}
