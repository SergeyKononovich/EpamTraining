using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Task3_АТS.Request;

namespace Task3_АТS.Response
{
    public interface IResponse
    {
        IRequest Request { get; }
        int Code { get; }
    }
}
