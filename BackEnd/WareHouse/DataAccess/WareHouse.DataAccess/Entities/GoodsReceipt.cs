using System;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Good receipt table.
    /// </summary>
    public class GoodsReceipt
    {
        public Guid Id { get; set; }
        public string ReceiptCode { get; set; }
        public string OrderNumber { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public bool IsReceipt { get; set; }
        public bool IsTransfer { get; set; }
        public bool IsTurnBack { get; set; }
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
