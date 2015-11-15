using System;

namespace Task3_АТS.ATS.Request
{
    public class MessageRequest : IRequest
    {
        public int Code => 0;
        public PhoneNumber Sender { get; }
        public string Message { get; }


        public MessageRequest(PhoneNumber sender, string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));


            Sender = sender;
            Message = message;
        }
    }
}
