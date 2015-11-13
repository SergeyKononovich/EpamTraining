using System;
using System.ComponentModel;
using Task3_АТS.Request;
using Task3_АТS.Response;
using Task3_АТS.Terminal;

namespace Task3_АТS.Port
{
    public abstract class PortBase : IPort
    {
        private PortState _state = PortState.Unplagged;
        public PortState State
        {
            get { return _state; }
            private set
            {
                if (_state != value)
                {
                    OnStateChanging(value);
                    PortState oldState = _state;
                    _state = value;
                    OnStateChanged(oldState);
                }
            }
        }


        public event EventHandler<PortState> StateChanging;
        public event StateChangedHandler<PortState> StateChanged;
        public event EventHandler<IRequest> ProcessRequestEvent;
        public event EventHandler<IResponse> ProcessResponseEvent;
        public event EventHandler<IRequest> SendRequestEvent;
        public event EventHandler<IResponse> SendResponseEvent;


        public virtual void RegisterTerminal(ITerminal terminal)
        {
            TerminalOnStateChanged(terminal, terminal.State, terminal.State);
            terminal.StateChanged += TerminalOnStateChanged;
            terminal.SendRequestEvent += SendRequest;
            terminal.SendResponseEvent += SendResponse;
        }
        public virtual void RemoveTerminal(ITerminal terminal)
        {
            State = PortState.Free;
            terminal.StateChanged -= TerminalOnStateChanged;
            terminal.SendRequestEvent -= SendRequest;
            terminal.SendResponseEvent -= SendResponse;
        }
        public void Plug()
        {
            State = PortState.Free;
        }
        public void Unplug()
        {
            State = PortState.Unplagged;
        }
        public void SendRequest(object sender, IRequest request)
        {
            if (request == null) return;


            if (State == PortState.Unplagged)
            {
                var r = new ConnectResponse(request, ConnectResponse.PortUnplagged);
                ProcessResponse(r);
                return;
            }

            OnSendRequestEvent(request);
        }
        public void SendResponse(object sender, IResponse response)
        {
            if (State == PortState.Unplagged || response == null) return;

            OnSendResponseEvent(response);
        }
        public virtual void ProcessRequest(IRequest request)
        {
            if (request == null) return;


            if (State == PortState.Unplagged)
            {
                var r = new ConnectResponse((ConnectRequest)request, ConnectResponse.PortUnplagged);
                OnSendResponseEvent(r);
                return;
            }

            if (request is ConnectRequest)
            {
                ConnectResponse r;
                if (State == PortState.Busy)
                {
                    r = new ConnectResponse((ConnectRequest)request, ConnectResponse.PortBusy);
                    OnSendResponseEvent(r);
                }
                return;
            }

            OnGetRequestEvent(request);
        }
        public virtual void ProcessResponse(IResponse response)
        {
            if (State != PortState.Free || response == null) return;

            OnGetResponseEvent(response);
        }
        public virtual void ClearEvents()
        {
            StateChanging = null;
            StateChanged = null;
            SendRequestEvent = null;
            SendResponseEvent = null;
        }

        protected void OnSendRequestEvent(IRequest request)
        {
            if (request == null) return;

            SendRequestEvent?.Invoke(this, request);
        }
        protected void OnSendResponseEvent(IResponse response)
        {
            if (response == null) return;

            SendResponseEvent?.Invoke(this, response);
        }

        private void OnStateChanging(PortState newState)
        {
            StateChanging?.Invoke(this, newState);
        }
        private void OnStateChanged(PortState oldState)
        {
            StateChanged?.Invoke(this, oldState, _state);
        }
        private void OnGetRequestEvent(IRequest request)
        {
            if (request == null) return;

            ProcessRequestEvent?.Invoke(this, request);
        }
        private void OnGetResponseEvent(IResponse response)
        {
            if (response == null) return;

            ProcessResponseEvent?.Invoke(this, response);
        }
    }
}
