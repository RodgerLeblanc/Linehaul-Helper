using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Exceptions
{
    public class IndeedJobsNotFoundException : Exception
    {
        public string HttpPageContent { get; private set; }

        public IndeedJobsNotFoundException()
        {
        }

        public IndeedJobsNotFoundException(string message)
            : base(message)
        {
        }

        public IndeedJobsNotFoundException(string message, string httpPageContent)
            : base(message)
        {
            HttpPageContent = httpPageContent;
        }

        public IndeedJobsNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public IndeedJobsNotFoundException(string message, string httpPageContent, Exception inner)
            : base(message, inner)
        {
            HttpPageContent = httpPageContent;
        }
    }
}
