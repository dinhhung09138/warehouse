using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Service.Models
{
    /// <summary>
    /// Department model.
    /// </summary>
    public class DepartmentModel
    {
        public string Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
