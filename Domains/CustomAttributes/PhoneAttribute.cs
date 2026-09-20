using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneAttribute:Attribute
    {

        public int Length { get; }

        public PhoneAttribute(int length = 0)
        {
            Length = length;
        }

    }
}
