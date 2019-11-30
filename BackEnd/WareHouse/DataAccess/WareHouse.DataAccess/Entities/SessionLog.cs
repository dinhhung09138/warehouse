using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Session log table.
    /// </summary>
    public class SessionLog
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool IsOnline { get; set; }
        public string IPAddress { get; set; }
        public string Platform { get; set; }
        public string Browser { get; set; }
        public string OSName { get; set; }
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
