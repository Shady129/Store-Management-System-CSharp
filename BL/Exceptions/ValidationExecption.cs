using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Execptions
{
    public class ValidationExecption:Exception
    {

        public string CustomMessage { get; set; }
        public ValidationExecption(string customMessage) : base()
        {
            CustomMessage = customMessage;
        }
        public ValidationExecption(string message, string customMessage) : base(message)
        {
            CustomMessage = customMessage;
        }
        public ValidationExecption(string message, string customMessage, Exception innerException) : base(message, innerException)
        {
            CustomMessage = customMessage;
        }

    }
}
