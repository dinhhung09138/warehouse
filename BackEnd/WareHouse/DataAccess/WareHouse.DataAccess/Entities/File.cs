using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.DataAccess.Entities
{
    /// <summary>
    /// File table.
    /// </summary>
    public class File
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileSystemName { get; set; }
        public string FilePath { get; set; }
        public decimal Size { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
