using System;
using Task3_АТS.ATS.Exceptions;
using Task3_АТS.ATS.NetworkEntity;
using Task3_АТS.ATS.Port;
using Task3_АТS.ATS.Request;
using Task3_АТS.ATS.Response;

namespace Task3_АТS.ATS.Terminal
{
    public abstract class TerminalBase : NetworkEntityBase, ITerminal
    {
        private TerminalState _state = TerminalState.Unplagged;
        private string _registeredPortMAC;


        public PhoneNumber PhoneNumber { get; set; }
        public TerminalState State
        {
            get { return _state; }
            private set
            {
                if (_state != value)
                {
                    OnStateChangingEvent(value);
                    TerminalState oldState = _state;
                    _state = value;
                    OnStateChangedEvent(oldState);
                }
            }
        }
        public bool IsPortRegistered => _registeredPortMAC != null;
        public bool IsRegisteredPortNotClosed { get; private set; }


        public event EventHandler<TerminalState> StateChangingEvent;
        public event StateChangedHandler<TerminalState> StateChangedEvent;
        public event EventHandler<IPort> RegisterPortEvent;
        public event EventHandler<IPort> RemovePortEvent;
        public event EventHandler<ConnectRequest> ConnectingEvent;
        public event EventHandler<ConnectResponse> ConnectSucceededEvent;
        public event EventHandler<ConnectResponse> ConnectFailedEvent;
        public event EventHandler DisconnectEvent;
        public event EventHandler<IRequest> SendRequestEvent;
        public event EventHandler<IResponse> SendResponseEvent;


        protected TerminalBase(string macAddress, PhoneNumber phoneNumber)
            : base(macAddress)
        {
            PhoneNumber = phoneNumber;
        }


        public virtual void RegisterPort(IPort port)
        {
            if (port == null) throw new ArgumentNullException(nameof(port));
            if (State == TerminalState.Unplagged)
                throw new StateException($"{this} unplagged");
            if (IsPortRegistered)
                throw new RegistrationException($"{this} already have registered port");

            
            port.StateChangedEvent += PortOnStateChangedEvent;
            PortOnStateChangedEvent(this, port.State, port.State);
            port.ProcessRequestEvent += ProcessRequest;
            port.ProcessResponseEvent += ProcessResponse;
            port.UnbindPortEvent += PortOnUnbindPortEvent;
            _registeredPortMAC = port.MACAddress;

            OnRegisterPort(port);
        }
        public virtual void RemovePort(IPort port)
        {
            if (port == null) throw new ArgumentNullException(nameof(port));
            if (State == TerminalState.Unplagged)
                throw new StateException($"{this} unplagged");
            if (port.MACAddress != _registeredPortMAC)
                throw new RegistrationException($"{this} did'n register {port}");


            if (!IsPortRegistered) return;

            port.StateChangedEvent -= PortOnStateChangedEvent;
            port.ProcessRequestEvent -= ProcessRequest;
            port.ProcessResponseEvent -= ProcessResponse;
            _registeredPortMAC = null;
            IsRegisteredPortNotClosed = false;
            State = TerminalState.Offline;

            OnRemovePort(port);
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
                throw new StateException($"{this} unplagged");
            if (State == TerminalState.Online)
                throw new StateException($"{this} already online");


            State = TerminalState.Online;
            var r = new ConnectRequest(ConnectRequest.UnprotectedConnection, PhoneNumber, phoneNumber);
            OnConnectingEvent(r);
            SendRequest(r);
        }
        public void Disconnect()
        {
            if (State == TerminalState.Unplagged)
                throw new StateException($"{this} unplagged");


            if (State != TerminalState.Online) return;

            State = TerminalState.Offline;
            OnDisconnectEvent();
        }
        public void SendRequest(IRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (State == TerminalState.Unplagged)
                throw new StateException($"{this} unplagged");


            if (!IsPortRegistered)
            {
                var r = new DropedResponse(request, DropedResponse.PortNotRegistered);
                ProcessResponse(this, r);
                return;
            }

            if (!IsRegisteredPortNotClosed)
            {
                var r = new DropedResponse(request, DropedResponse.PortUnplagged);
                ProcessResponse(this, r);
                return;
            }

            OnSendRequestEvent(request);
        }
        public void SendResponse(IResponse response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (State == TerminalState.Unplagged)
                throw new StateException($"{this} unplagged");


            if (!IsPortRegistered)
            {
                var r = new DropedResponse(response.Request, DropedResponse.PortNotRegistered);
                ProcessResponse(this, r);
                return;
            }

            if (!IsRegisteredPortNotClosed)
            {
                var r = new DropedResponse(response.Request, DropedResponse.PortUnplagged);
                ProcessResponse(this, r);
                return;
            }

            OnSendResponseEvent(response);
        }
        public void ProcessRequest(object sender, IRequest request)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (State == TerminalState.Unplagged)
                throw new StateException($"{this} unplagged");


            var senderMac = sender as string;
            if (senderMac == null || (senderMac != _registeredPortMAC && senderMac != MACAddress))
                return;

            if (request is ConnectRequest)
            {
                if (State == TerminalState.Online)
                {
                    var r = new ConnectResponse((ConnectRequest)request, ConnectResponse.TerminalBusy);
                    SendResponse(r);
                }
                else
                {
                    State = TerminalState.Online;
                    var r = new ConnectResponse((ConnectRequest)request, ConnectResponse.TerminalOk);
                    SendResponse(r);
                }
                return;
            }

            if (State == TerminalState.Offline) return;

            ProcessRequestWhenConnect(request);
        }
        public void ProcessResponse(object sender, IResponse response)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (State == TerminalState.Unplagged)
                throw new StateException($"{this} unplagged");


            var senderMac = sender as string;
            if (senderMac == null || (senderMac != _registeredPortMAC && senderMac != MACAddress))
                return;

            if (State == TerminalState.Offline) return;

            if (response is ConnectResponse)
            {
                var r = (ConnectResponse) response;
                if (response.Code == ConnectResponse.TerminalOk)
                    OnConnectSucceededEvent(r);
                else
                {
                    State = TerminalState.Offline;
                    OnConnectFailedEvent(r);
                }
            }

            ProcessResponseWhenConnect(response);
        }
        public virtual void ClearEvents()
        {
            StateChangingEvent = null;
            StateChangedEvent = null;
            RegisterPortEvent = null;
            RemovePortEvent = null;
            ConnectingEvent = null;
            ConnectSucceededEvent = null;
            ConnectFailedEvent = null;
            DisconnectEvent = null;
            SendRequestEvent = null;
            SendResponseEvent = null;
        }
        public override string ToString()
        {
            return $"Terminal: {base.ToString()}";
        }

        protected abstract void ProcessRequestWhenConnect(IRequest request);
        protected abstract void ProcessResponseWhenConnect(IResponse response);

        private void OnStateChangingEvent(TerminalState terminalState)
        {
            StateChangingEvent?.Invoke(MACAddress, terminalState);
        }
        private void OnStateChangedEvent(TerminalState oldstate)
        {
            StateChangedEvent?.Invoke(MACAddress, oldstate, _state);
        }
        private void OnRegisterPort(IPort port)
        {
            RegisterPortEvent?.Invoke(MACAddress, port);
        }
        private void OnRemovePort(IPort port)
        {
            RemovePortEvent?.Invoke(MACAddress, port);
        }
        private void OnConnectingEvent(ConnectRequest request)
        {
            ConnectingEvent?.Invoke(MACAddress, request);
        }
        private void OnConnectSucceededEvent(ConnectResponse response)
        {
            ConnectSucceededEvent?.Invoke(MACAddress, response);
        }
        private void OnConnectFailedEvent(ConnectResponse response)
        {
            ConnectFailedEvent?.Invoke(MACAddress, response);
        }
        private void OnDisconnectEvent()
        {
            DisconnectEvent?.Invoke(MACAddress, EventArgs.Empty);
        }
        private void OnSendRequestEvent(IRequest request)
        {
            SendRequestEvent?.Invoke(MACAddress, request);
        }
        private void OnSendResponseEvent(IResponse response)
        {
            SendResponseEvent?.Invoke(MACAddress, response);
        }

        private void PortOnStateChangedEvent(object sender, PortState oldState, PortState newState)
        {
            IsRegisteredPortNotClosed = newState != PortState.Close;

            if (IsRegisteredPortNotClosed == false && State != TerminalState.Unplagged)
                Disconnect();

        }
        private void PortOnUnbindPortEvent(object sender, IPort port)
        {
            if (State != TerminalState.Unplagged)
                Disconnect();
        }
    }
}
