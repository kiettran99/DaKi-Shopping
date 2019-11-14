using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopDaki.Models
{
    [Table("Product")]
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public float Price { get; set; }

        public string Images { get; set; }
        public string PriceNew { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Group Product")]
        public int GroupProductID { get; set; }
        [ForeignKey("GroupProductID")]
        public virtual GroupProduct GroupProduct { get; set; }
    }
}
