using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Service.Models
{
    /// <summary>
    /// User model.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Employee id.
        /// </summary>
        [Required(ErrorMessage = Constants.Message.EmployeeIsRequired)]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Employee name.
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// UserName.
        /// </summary>
        [Required(ErrorMessage = Constants.Message.UserNameIsRequired)]
        public string UserName { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required(ErrorMessage = Constants.Message.PasswordIsRequired)]
        public string Password { get; set; }

        /// <summary>
        /// Active status.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Last login time.
        /// </summary>
        public DateTime? LastLogin { get; set; }

        public string CurrentUserId { get; set; }
    }
}
