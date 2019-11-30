using System;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Goods issue table.
    /// </summary>
    public class GoodsIssue
    {
        public Guid Id { get; set; }
        public string IssueCode { get; set; }
        public string OrderNumber { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeId { get; set; }
        public Guid WarehouseId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public bool IsIssue { get; set; }
        public bool IsTransfer { get; set; }
        public bool IsActive { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Deleted { get; set; }
        public string DeleteBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
