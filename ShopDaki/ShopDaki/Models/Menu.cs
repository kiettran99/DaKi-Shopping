using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopDaki.Models
{
    [Table("Menu")]
    public class Menu
    {
        public int MenuID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
