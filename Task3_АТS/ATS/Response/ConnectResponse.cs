using System;
using Task3_АТS.ATS.Request;

namespace Task3_АТS.ATS.Response
{
    public class ConnectResponse : IResponse
    {
        public const int TerminalOk = 1111;
        public const int TerminalBusy = 2222;

        public ConnectRequest ConnectRequest { get; }
        public IRequest Request => ConnectRequest;
        public int Code { get; }


        public ConnectResponse(ConnectRequest request, int code)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            ConnectRequest = request;
            Code = code;
        }
    }
}
