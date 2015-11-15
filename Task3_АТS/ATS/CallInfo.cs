using System;

namespace Task3_АТS.ATS
{
    public class CallInfo
    {
        public PhoneNumber Source { get; set; }
        public PhoneNumber Target { get; set; }
        public DateTime Started { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}
