using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3_АТS
{
    public class CallInfo
    {

        public PhoneNumber Source { get; set; }
        public PhoneNumber Target { get; set; }
        public DateTime Started { get; set; }
        public TimeSpan Duration { get; set; }

    }
}
