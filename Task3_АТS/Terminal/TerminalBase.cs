using System;
using System.Collections.Generic;
using System.Data;
using Task3_АТS.Exceptions;
using Task3_АТS.Port;
using Task3_АТS.Request;
using Task3_АТS.Response;

namespace Task3_АТS.Terminal
{
    public abstract class TerminalBase : ITerminal
    {
        private TerminalState _state = TerminalState.Unplagged;


        public string Number { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public TerminalState State
        {
            get { return _state; }
            private set
            {
                if (_state != value)
                {
                    OnStateChanging(value);
                    TerminalState oldState = _state;
                    _state = value;
                    OnStateChanged(oldState);
                }
            }
        }

        public bool IsPortConnect { get; private set; }


        public event EventHandler<TerminalState> StateChanging;
        public event StateChangedHandler<TerminalState> StateChanged;
        public event EventHandler RegisterPortEvent;
        public event EventHandler RemovePortEvent;
        public event EventHandler<ConnectRequest> ConnectingEvent;
        public event EventHandler<ConnectResponse> ConnectSucceededEvent;
        public event EventHandler<ConnectResponse> ConnectFailedEvent;
        public event EventHandler<DisconnectRequest> DisconnectEvent;
        public event EventHandler<IRequest> SendRequestEvent;
        public event EventHandler<IResponse> SendResponseEvent;


        protected TerminalBase(string number, PhoneNumber phoneNumber)
        {
            if (number == null) throw new ArgumentNullException(nameof(number));

            Number = number;
            PhoneNumber = phoneNumber;
        }


        public virtual void RegisterPort(IPort port)
        {
            if (IsPortConnect) return;

            OnRegisterPort();
        }
        public virtual void RemovePort(IPort port)
        {
            if (!IsPortConnect) return;
            

            OnRemovePort();
        }
        public void Plug()
        {
            State = TerminalState.Offline;
        }
        public void Unplug()
        {
            Disconnect();
            State = TerminalState.Unplagged;
        }
        public void Connect(PhoneNumber phoneNumber)
        {
            if (State == TerminalState.Unplagged)
                throw new StateException($"Terminal {this} unplagged");
            if (State == TerminalState.Online)
                throw new StateException($"Terminal {this} already online");


            var r = new ConnectRequest(ConnectRequest.UnprotectedConnection, PhoneNumber, phoneNumber);
            OnConnectingEvent(r);
            OnSendRequestEvent(r);
        }
        public void Disconnect()
        {
            if (State != TerminalState.Online) return;


            var r = new DisconnectRequest(DisconnectRequest.Disconnect);
            OnSendRequestEvent(r);
            OnDisconnectEvent(r);
        }
        public virtual void ProcessRequest(IRequest request)
        {
            if (request == null) return;


            if (State == TerminalState.Unplagged)
            {
                var r = new ConnectResponse(request, ConnectResponse.TerminalUnplagged);
                OnSendResponseEvent(r);
                return;
            }


            if (request is ConnectRequest)
            {
                ConnectResponse r;
                if (State == TerminalState.Online)
                {
                    r = new ConnectResponse(request, ConnectResponse.TerminalBusy);
                    OnSendResponseEvent(r);
                }
                else
                {
                    r = new ConnectResponse(request, ConnectResponse.TerminalOk);
                    OnSendResponseEvent(r);
                    OnConnectSucceededEvent(r);
                }
                return;
            }


            if (request is DisconnectRequest)
            {
                OnDisconnectEvent((DisconnectRequest) request);
            }
        }
        public virtual void ProcessResponse(IResponse response)
        {
            if (State != TerminalState.Online || response == null) return;

            if (response is ConnectResponse)
            {
                var r = (ConnectResponse) response;
                if (response.Code == ConnectResponse.TerminalOk)
                    OnConnectSucceededEvent(r);
                else
                    OnConnectFailedEvent(r);
            }
        }
        public virtual void ClearEvents()
        {
            StateChanging = null;
            StateChanged = null;
            RegisterPortEvent = null;
            RemovePortEvent = null;
            ConnectingEvent = null;
            ConnectSucceededEvent = null;
            ConnectFailedEvent = null;
            DisconnectEvent = null;
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

        private void OnStateChanging(TerminalState e)
        {
            StateChanging?.Invoke(this, e);
        }
        private void OnStateChanged(TerminalState oldstate)
        {
            StateChanged?.Invoke(this, oldstate, _state);
        }
        private void OnRegisterPort()
        {
            IsPortConnect = true;

            RegisterPortEvent?.Invoke(this, EventArgs.Empty);
        }
        private void OnRemovePort()
        {
            IsPortConnect = false;
            IsPortFree = false;
            OnOffline();
            RemovePortEvent?.Invoke(this, EventArgs.Empty);
        }
        private void OnConnectingEvent(ConnectRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            State = TerminalState.Online;
            ConnectingEvent?.Invoke(this, request);
        }
        private void OnConnectSucceededEvent(ConnectResponse response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            ConnectSucceededEvent?.Invoke(this, response);
        }
        private void OnConnectFailedEvent(ConnectResponse response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            State = TerminalState.Offline;
            ConnectFailedEvent?.Invoke(this, response);
        }
        private void OnDisconnectEvent(DisconnectRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            State = TerminalState.Offline;
            DisconnectEvent?.Invoke(this, request);
        }

        // for register port
        //private void PortStateChanged(object sender, PortState oldState, PortState newState)
        //{
        //    //IsPortUnPlagged = newState == PortState.Unplagged;
        //}
    }
}
