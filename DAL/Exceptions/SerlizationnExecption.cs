using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class SerlizationnExecption:Exception
    {


        public string CustomMessage { get; set; }



        public SerlizationnExecption(string customMessage, string message,
            Exception innerException) : base(message, innerException)
        {
            CustomMessage = customMessage;
        }

    }
}
