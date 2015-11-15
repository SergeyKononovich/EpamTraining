using Task3_АТS.ATS.Request;

namespace Task3_АТS.ATS.Response
{
    public interface IResponse
    {
        IRequest Request { get; }
        int Code { get; }
    }
}
