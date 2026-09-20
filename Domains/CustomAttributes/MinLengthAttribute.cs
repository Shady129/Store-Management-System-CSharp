using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.CustomAttributes
{
   

    [AttributeUsage(AttributeTargets.Property)]
    public class MinLengthAttribute:Attribute
    {
        public int Length { get; }

        public MinLengthAttribute(int length)
        {
            Length = length;
        }

    }
}
