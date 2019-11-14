using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Models
{
    [Table("Company")]
   
        public class Company
        {
            public int CompanyID { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string Fax { get; set; }
        }
    }

