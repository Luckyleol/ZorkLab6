using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zork
{
    internal static class Assert
    {
        [Conditional("DEBUG")]
        public static void IsTrue(bool experssion,string message = null)
        {
            if (experssion == false)
            {
                throw new Exception(message);
            }
        }
    }
}
