using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Goods issue location table.
    /// </summary>
    public class GoodsIssueLocation
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? CustomerStoreId { get; set; }
        public Guid? WareHouseId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalCost { get; set; }
        public string Notes { get; set; }
    }
}
