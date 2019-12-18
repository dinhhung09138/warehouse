using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Service.Models
{
    /// <summary>
    /// Customer employee model.
    /// </summary>
    public class CustomerEmployeeModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(300)]
        public string Name { get; set; }

        public string AvatarFileId { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(250)]
        public string Password { get; set; }

        public DateTime? StartOn { get; set; }

        public string CustomerStoreId { get; set; }

        public string CustomerStoreName { get; set; }

        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
