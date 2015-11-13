using System;
using Task3_АТS.Request;

namespace Task3_АТS.Response
{
    public class ConnectResponse : IResponse
    {
        public const int TerminalOk = 1111;
        public const int TerminalRejected = 7878;
        public const int TerminalUnplagged = 1234;
        public const int TerminalBusy = 2222;

        public const int PortUnplagged = 4254;
        public const int PortBusy = 3765;
        public const int TargetPortUnplagged = 8887;
        public const int TargetPortBusy = 8762;

        public IRequest Request { get; }
        public int Code { get; }


        public ConnectResponse(IRequest request, int code)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Request = request;
            Code = code;
        }
    }
}
