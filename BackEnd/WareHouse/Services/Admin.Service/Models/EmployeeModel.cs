using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Service.Models
{
    /// <summary>
    /// Employee model.
    /// </summary>
    public class EmployeeModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public string AvatarFileId { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; }

        [MaxLength(20)]
        public string WorkPhone { get; set; }

        [MaxLength(20)]
        public string Fax { get; set; }

        public DateTime DateOfJoin { get; set; }

        public DateTime? DateOfLeaving { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public string DepartmentId { get; set; }

        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
