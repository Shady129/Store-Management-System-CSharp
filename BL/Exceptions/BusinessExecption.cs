using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Execptions
{
    public class BusinessExecption:Exception
    {

        public string CustomMessage { get; set; }
        public BusinessExecption(string customMessage) : base()
        {
            CustomMessage = customMessage;
        }
        public BusinessExecption(string message, string customMessage) : base(message)
        {
            CustomMessage = customMessage;
        }
        public BusinessExecption(string message, string customMessage, Exception innerException) : base(message, innerException)
        {
            CustomMessage = customMessage;
        }

    }
}
