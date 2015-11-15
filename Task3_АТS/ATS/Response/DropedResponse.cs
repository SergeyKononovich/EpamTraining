using System;
using Task3_АТS.ATS.Request;

namespace Task3_АТS.ATS.Response
{
    public class DropedResponse : IResponse
    {
        public const int TerminalNotRegistered = 6333;
        public const int TerminalUnplagged = 1234;
        public const int TerminalBusy = 2222;

        public const int PortNotRegistered = 6345;
        public const int PortUnbind = 3332;
        public const int PortUnplagged = 4254;

        public const int TargetPortUnplagged = 8887;

        public IRequest Request { get; }

        public int Code { get; }


        public DropedResponse(IRequest request, int code)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Request = request;
            Code = code;
        }
    }
}
