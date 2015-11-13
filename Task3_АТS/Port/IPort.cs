using System;
using Task3_АТS.Request;
using Task3_АТS.Response;
using Task3_АТS.Terminal;

namespace Task3_АТS.Port
{
    public enum PortState
    {
        Free,
        Busy,
        Unplagged
    }

    public interface IPort
    {
        PortState State { get; }

        event EventHandler<PortState> StateChanging;
        event StateChangedHandler<PortState> StateChanged;
        event EventHandler<IRequest> ProcessRequestEvent;
        event EventHandler<IResponse> ProcessResponseEvent;
        event EventHandler<IRequest> SendRequestEvent;
        event EventHandler<IResponse> SendResponseEvent;

        void RegisterTerminal(ITerminal terminal);
        void RemoveTerminal(ITerminal terminal);
        void Plug();
        void Unplug();
        void SendRequest(object sender, IRequest request);
        void SendResponse(object sender, IResponse response);
        void ProcessRequest(IRequest request);
        void ProcessResponse(IResponse response);
        void ClearEvents();
    }
}
