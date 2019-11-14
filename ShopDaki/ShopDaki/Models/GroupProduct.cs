using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Models
{
    [Table("GroupProduct")]
    public class GroupProduct
    {
        public int GroupProductID { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
    }
}
