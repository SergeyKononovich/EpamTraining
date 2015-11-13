namespace Task3_АТS.Request
{
    public class DisconnectRequest : IRequest
    {
        public const int Disconnect = 8765;
        public const int TargetPortUnpluged = 4533;
        public const int TargetUnpluged = 8980;

        public int Code { get; }


        public DisconnectRequest(int code)
        {
            Code = code;
        }
    }
}
