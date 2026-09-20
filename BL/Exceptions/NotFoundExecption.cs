using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Execptions
{
    public class NotFoundExecption:Exception
    {
        public string CustomMessage { get; set; }
        public NotFoundExecption(string customMessage) : base()
        {
            CustomMessage = customMessage;
        }
        public NotFoundExecption(string message, string customMessage) : base(message)
        {
            CustomMessage = customMessage;
        }
        public NotFoundExecption(string message, string customMessage, Exception innerException) : base(message, innerException)
        {
            CustomMessage = customMessage;
        }


    }
}
