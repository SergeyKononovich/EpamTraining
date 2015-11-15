using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3_АТS.BillingSystem
{
    public interface IBillingSystem
    {
        void ProcessConnectionFailed(object sender, CallInfo call);
        void ProcessCall(object sender, CallInfo call);
    }
}
