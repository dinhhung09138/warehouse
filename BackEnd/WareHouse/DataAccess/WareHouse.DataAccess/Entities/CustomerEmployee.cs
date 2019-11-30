using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Customer employee table.
    /// </summary>
    public class CustomerEmployee
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid? AvatarFileId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? StartOn { get; set; }
        public Guid? CustomerStoreId { get; set; }
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
