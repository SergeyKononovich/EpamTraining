using System;
using Task3_АТS.ATS.NetworkEntity;
using Task3_АТS.ATS.Port;
using Task3_АТS.ATS.Request;
using Task3_АТS.ATS.Response;

namespace Task3_АТS.ATS.Terminal
{
    public enum TerminalState
    {
        Online,
        Offline,
        Unplagged
    }

    public interface ITerminal : INetworkEntity
    {
        PhoneNumber PhoneNumber { get; set; }
        TerminalState State { get; }
        bool IsPortRegistered { get; }


        event EventHandler<TerminalState> StateChangingEvent;
        event StateChangedHandler<TerminalState> StateChangedEvent;
        event EventHandler<IPort> RegisterPortEvent;
        event EventHandler<IPort> RemovePortEvent;
        event EventHandler<ConnectRequest> ConnectingEvent;
        event EventHandler<ConnectResponse> ConnectSucceededEvent;
        event EventHandler<ConnectResponse> ConnectFailedEvent;
        event EventHandler DisconnectEvent;
        event EventHandler<IRequest> SendRequestEvent;
        event EventHandler<IResponse> SendResponseEvent;


        void RegisterPort(IPort port);
        void RemovePort(IPort port);
        void Plug();
        void Unplug();
        void Connect(PhoneNumber phoneNumber);
        void Disconnect();
        void SendRequest(IRequest request);
        void SendResponse(IResponse response);
        void ProcessRequest(object sender, IRequest request);
        void ProcessResponse(object sender, IResponse response);
        void ClearEvents();
    }
}
