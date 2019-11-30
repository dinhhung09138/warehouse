using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Goods Issue detail table.
    /// </summary>
    public class GoodsIssueDetail
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid GoodsId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public string Notes { get; set; }
    }
}
