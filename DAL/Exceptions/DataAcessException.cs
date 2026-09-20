using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class DataAcessException:Exception
    {


        public string CustomMessage { get; set; }

        public DataAcessException(
       string message,
       string customMessage,
       Exception innerException): base(message, innerException)
        {
            CustomMessage = customMessage;
        }
    }
}
