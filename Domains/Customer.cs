using System;
using System.Collections.Generic;
using Domains.CustomAttributes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Customer : BaseEntity
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string CustomerName { get; set; }


        [Required]
        [Email]
        public string Email { get; set; }

        [Required]
        [Phone(11)]
        public string Phone { get; set; }

    }
}
