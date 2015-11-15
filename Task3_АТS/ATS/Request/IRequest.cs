namespace Task3_АТS.ATS.Request
{
    public interface IRequest
    {
        int Code { get; }
        PhoneNumber Sender { get; }
    }
}
