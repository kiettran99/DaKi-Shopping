using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopDaki.Models
{
    [Table("Province")]
    public class Province
    {
        public int ProvinceID { get; set; }
        public string Name { get; set; }
    }
}
