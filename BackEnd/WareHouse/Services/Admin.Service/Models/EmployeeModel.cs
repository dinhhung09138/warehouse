using Core.Common.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string AvatarFileId { get; set; }

        public string AvatarContent { get; set; }

        public IFormFile File { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; }

        [MaxLength(20)]
        public string WorkPhone { get; set; }

        [MaxLength(20)]
        public string Fax { get; set; }

        public DateTime DateOfJoin { get; set; }

        public string DateOfJoinString { get; set; }

        public DateTime? DateOfLeaving { get; set; }

        public string DateOfLeavingString { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public string DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        [Required]
        public string IsActive { get; set; } = "1";

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public string IsEdit { get; set; } = FormStatus.Insert;
    }
}
