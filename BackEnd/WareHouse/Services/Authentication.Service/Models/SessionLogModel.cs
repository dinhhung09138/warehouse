using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Service.Models
{
    /// <summary>
    /// Session log model.
    /// </summary>
    public class SessionLogModel
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool IsOnline { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLogin { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeleteBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
