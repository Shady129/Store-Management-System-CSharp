using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.CustomAttributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class RangeAttribute:Attribute
    {


        public decimal Min { get; set; }

        public decimal Max { get; set; }


        public RangeAttribute(double min, double max)
        {

            Min = (decimal)min;
            Max = (decimal)max;

        }














    }
}
