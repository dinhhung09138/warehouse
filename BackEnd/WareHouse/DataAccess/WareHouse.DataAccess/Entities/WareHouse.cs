using System;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Warehouse table.
    /// </summary>
    public class WareHouse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public Guid? CitiId { get; set; }
        public Guid? CountryId { get; set; }
        public decimal Longtitue { get; set; }
        public decimal Latitude { get; set; }
        public Guid StockKeeperId { get; set; }
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
