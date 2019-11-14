using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopDaki.Models
{
    [Table("Shop")]
    public class Shop
    {
        public int ShopID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string HotLine { get; set; }

        public int ProvinceID { get; set; }
        public Province Province { get; set; }
    }
}
