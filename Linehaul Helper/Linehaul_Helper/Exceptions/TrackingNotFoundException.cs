using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Exceptions
{
    public class TrackingNotFoundException : Exception
    {
        public string HttpPageContent { get; private set; }

        public TrackingNotFoundException()
        {
        }

        public TrackingNotFoundException(string message)
            : base(message)
        {
        }

        public TrackingNotFoundException(string message, string httpPageContent)
            : base(message)
        {
            HttpPageContent = httpPageContent;
        }

        public TrackingNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public TrackingNotFoundException(string message, string httpPageContent, Exception inner)
            : base(message, inner)
        {
            HttpPageContent = httpPageContent;
        }
    }
}
