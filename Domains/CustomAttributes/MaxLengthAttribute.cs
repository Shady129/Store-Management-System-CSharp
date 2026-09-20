using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.CustomAttributes
{
   public class MaxLengthAttribute:Attribute
    {
        public int Length { get; }

        public MaxLengthAttribute(int length)
        {

           Length = length;
        }
    }
}
