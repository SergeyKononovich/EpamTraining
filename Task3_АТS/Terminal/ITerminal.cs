using System;
using Task3_АТS.Port;
using Task3_АТS.Request;
using Task3_АТS.Response;

namespace Task3_АТS.Terminal
{
    public enum TerminalState
    {
        Online,
        Offline,
        Unplagged
    }

    public interface ITerminal
    {
        string Number { get; set; }
        PhoneNumber PhoneNumber { get; set; }
        TerminalState State { get; }
        bool IsPortConnect { get; }


        event EventHandler<TerminalState> StateChanging;
        event StateChangedHandler<TerminalState> StateChanged;
        event EventHandler RegisterPortEvent;
        event EventHandler RemovePortEvent;
        event EventHandler<ConnectRequest> ConnectingEvent;
        event EventHandler<ConnectResponse> ConnectSucceededEvent;
        event EventHandler<ConnectResponse> ConnectFailedEvent;
        event EventHandler<DisconnectRequest> DisconnectEvent;
        event EventHandler<IRequest> SendRequestEvent;
        event EventHandler<IResponse> SendResponseEvent;


        void RegisterPort(IPort port);
        void RemovePort(IPort port);
        void Plug();
        void Unplug();
        void Connect(PhoneNumber phoneNumber);
        void ProcessRequest(IRequest request);
        void ProcessResponse(IResponse response);
        void ClearEvents();
    }
}
