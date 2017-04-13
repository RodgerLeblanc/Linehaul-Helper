using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Exceptions
{
    public class LayoutException : Exception
    {
        public LayoutException(string message)
            : base(message)
        {
        }
    }
}
