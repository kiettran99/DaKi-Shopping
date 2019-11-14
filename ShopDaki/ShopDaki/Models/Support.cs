using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Models
{
    [Table("Support")]
    public class Support
    {
        public int SupportID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Type { get; set; }
        public string NickName { get; set; }
        public string Status { get; set; }
    }
}
