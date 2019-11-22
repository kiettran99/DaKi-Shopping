using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDaki.Models
{

    [Table("Contact")]


    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
    }
}

