using Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Service.Models
{
    public class CustomerFilterModel : FilterModel
    {
        public string CustomerId { get; set; }
        public string ClientId { get; set; }
    }
}
