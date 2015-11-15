using System;
using Task3_АТS.ATS.Exceptions;
using Task3_АТS.ATS.NetworkEntity;
using Task3_АТS.ATS.Port;
using Task3_АТS.ATS.Request;
using Task3_АТS.ATS.Response;

namespace Task3_АТS.ATS.Terminal
{
    public class Terminal : NetworkEntityBase, ITerminal
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
        public event EventHandler<DropedResponse> DropedEvent;
        public event EventHandler<PhoneNumber> DisconnectEvent;
        public event EventHandler<IRequest> SendRequestEvent;
        public event EventHandler<IResponse> SendResponseEvent;


        protected Terminal(string macAddress, PhoneNumber phoneNumber)
            : base(macAddress)
        {
            PhoneNumber = phoneNumber;
        }


        public virtual void RegisterPort(IPort port)
        {
            if (port == null) throw new ArgumentNullException(nameof(port));
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
            if (port.MACAddress != _registeredPortMAC)
                throw new RegistrationException($"{this} did'n register {port}");


            if (!IsPortRegistered) return;

            port.StateChangedEvent -= PortOnStateChangedEvent;
            port.ProcessRequestEvent -= ProcessRequest;
            port.ProcessResponseEvent -= ProcessResponse;
            port.UnbindPortEvent -= PortOnUnbindPortEvent;
            _registeredPortMAC = null;
            IsRegisteredPortNotClosed = false;

            if (State != TerminalState.Unplagged)
                State = TerminalState.Offline;
            OnRemovePort(port);
        }
        public void Plug()
        {
            if (State != TerminalState.Unplagged) return;

            State = TerminalState.Offline;
        }
        public void Unplug()
        {
            if (State == TerminalState.Unplagged) return;
            
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


            if (State == TerminalState.Offline) return;

            if (!IsPortRegistered)
            {
                var r = new DropedResponse(request, DropedResponse.PortNotRegistered);
                ProcessResponse(MACAddress, r);
                return;
            }

            if (!IsRegisteredPortNotClosed)
            {
                var r = new DropedResponse(request, DropedResponse.PortClose);
                ProcessResponse(MACAddress, r);
                return;
            }

            OnSendRequestEvent(request);
        }
        public void SendResponse(IResponse response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (State == TerminalState.Unplagged)
                throw new StateException($"{this} unplagged");


            if (State == TerminalState.Offline) return;

            if (!IsPortRegistered)
            {
                var r = new DropedResponse(response.Request, DropedResponse.PortNotRegistered);
                ProcessResponse(MACAddress, r);
                return;
            }

            if (!IsRegisteredPortNotClosed)
            {
                var r = new DropedResponse(response.Request, DropedResponse.PortClose);
                ProcessResponse(MACAddress, r);
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
            if (senderMac == null || senderMac != _registeredPortMAC) return;

            if (request is ConnectRequest)
            {
                if (State == TerminalState.Online)
                {
                    var r = new ConnectResponse((ConnectRequest)request, ConnectResponse.TerminalBusy);
                    OnSendResponseEvent(r);
                }
                else
                {
                    State = TerminalState.Online;
                    var r = new ConnectResponse((ConnectRequest)request, ConnectResponse.TerminalOk);
                    OnSendResponseEvent(r);
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


            if (State == TerminalState.Offline) return;

            var senderMac = sender as string;
            if (senderMac == null || (senderMac != _registeredPortMAC && senderMac != MACAddress))
                return;

            if (response is ConnectResponse)
            {
                if (response.Code == ConnectResponse.TerminalOk)
                    OnConnectSucceededEvent((ConnectResponse)response);
                else
                {
                    State = TerminalState.Offline;
                    OnConnectFailedEvent((ConnectResponse)response);
                }

                return;
            }

            if (response is DropedResponse)
            {
                State = TerminalState.Offline;
                OnDropedEvent((DropedResponse)response);

                return;
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
            return $"Terminal[{base.ToString()}]";
        }

        protected virtual void ProcessRequestWhenConnect(IRequest request)
        { }
        protected virtual void ProcessResponseWhenConnect(IResponse response)
        { }

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
        private void OnDropedEvent(DropedResponse e)
        {
            DropedEvent?.Invoke(this, e);
        }
        private void OnDisconnectEvent()
        {
            DisconnectEvent?.Invoke(MACAddress, PhoneNumber);
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
        }
        private void PortOnUnbindPortEvent(object sender, IPort port)
        {
            if (State != TerminalState.Unplagged)
                State = TerminalState.Offline;
        }
    }
}
