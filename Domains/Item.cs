using Domains.CustomAttributes;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Item:BaseEntity
    {

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; } = "";


        [Required]
        [Range(10, 500)]
        public decimal Price { get; set; }


        [Required]
        [Range(0, 1000)]
        public int Quantity { get; set; }



    }
}
