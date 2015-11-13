namespace Task3_АТS.Request
{
    public class ConnectRequest : IRequest
    {
        public const int UnprotectedConnection = 4546;
        public const int EncryptedConnection = 1234;

        public int Code { get; }
        public PhoneNumber Sender { get; }
        public PhoneNumber Target { get; }


        public ConnectRequest(int code, PhoneNumber sender, PhoneNumber target)
        {
            Code = code;
            Sender = sender;
            Target = target;
        }
    }
}
