using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// Employee table.
    /// </summary>
    public class Employee
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AvatarFileId { get; set; }
        public string Mobile { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }
        public DateTime DateOfJoin { get; set; }
        public DateTime? DateOfLeaving { get; set; }
        public string Email { get; set; }
        public Guid? DepartmentId { get; set; }
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
