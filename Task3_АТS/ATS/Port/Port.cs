using System;
using Task3_АТS.ATS.Exceptions;
using Task3_АТS.ATS.NetworkEntity;
using Task3_АТS.ATS.Request;
using Task3_АТS.ATS.Response;
using Task3_АТS.ATS.Terminal;

namespace Task3_АТS.ATS.Port
{
    public class Port : NetworkEntityBase, IPort
    {
        private PortState _state = PortState.Close;
        private string _registeredTerminalMAC;
        private string _bindedPortMAC;


        public PortState State
        {
            get { return _state; }
            private set
            {
                if (_state != value)
                {
                    OnStateChangingEvent(value);
                    PortState oldState = _state;
                    _state = value;
                    OnStateChangedEvent(oldState);
                }
            }
        }
        public bool IsTerminalRegistered => _registeredTerminalMAC != null;
        public bool IsRegisteredTerminalPluged { get; private set; }
        public bool IsPortBinded => _bindedPortMAC != null;
        public bool IsBindedPortNotClosed { get; private set; }


        public event EventHandler<PortState> StateChangingEvent;
        public event StateChangedHandler<PortState> StateChangedEvent;
        public event EventHandler<ITerminal> RegisterTerminalEvent;
        public event EventHandler<ITerminal> RemoveTerminalEvent;
        public event EventHandler<IPort> BindPortEvent;
        public event EventHandler<IPort> UnbindPortEvent;
        public event EventHandler<IRequest> ProcessRequestEvent;
        public event EventHandler<IResponse> ProcessResponseEvent;
        public event EventHandler<IRequest> SendRequestEvent;
        public event EventHandler<IResponse> SendResponseEvent;


        protected Port(string macAddress)
            : base(macAddress) { }


        public virtual void RegisterTerminal(ITerminal terminal)
        {
            if (terminal == null) throw new ArgumentNullException(nameof(terminal));
            if (IsTerminalRegistered)
                throw new RegistrationException($"{this} already have registered terminal");


            terminal.StateChangedEvent += TerminalOnStateChangedEvent;
            TerminalOnStateChangedEvent(this, terminal.State, terminal.State);
            terminal.SendRequestEvent += SendRequest;
            terminal.SendResponseEvent += SendResponse;
            _registeredTerminalMAC = terminal.MACAddress;

            OnRegisterTerminalEvent(terminal);
        }
        public virtual void RemoveTerminal(ITerminal terminal)
        {
            if (terminal == null) throw new ArgumentNullException(nameof(terminal));
            if (terminal.MACAddress != _registeredTerminalMAC)
                throw new RegistrationException($"{this} did'n register {terminal}");


            terminal.StateChangedEvent -= TerminalOnStateChangedEvent;
            IsRegisteredTerminalPluged = false;
            terminal.SendRequestEvent -= SendRequest;
            terminal.SendResponseEvent -= SendResponse;
            _registeredTerminalMAC = null;

            OnRemoveTerminalEvent(terminal);
        }
        public virtual void BindPort(IPort port)
        {
            if (port == null) throw new ArgumentNullException(nameof(port));
            if (IsPortBinded)
                throw new RegistrationException($"{this} already binded");

            
            port.StateChangedEvent += PortOnStateChangedEvent;
            PortOnStateChangedEvent(this, port.State, port.State);
            port.SendRequestEvent += ProcessRequest;
            port.SendResponseEvent += ProcessResponse;
            _bindedPortMAC = port.MACAddress;

            if (State != PortState.Close)
                State = PortState.Listened;
            OnBindPortEvent(port);
        }
        public virtual void UnbindPort(IPort port)
        {
            if (port == null) throw new ArgumentNullException(nameof(port));
            if (port.MACAddress != _bindedPortMAC)
                throw new RegistrationException($"{this} did'n bind to {port}");


            port.StateChangedEvent -= PortOnStateChangedEvent;
            IsBindedPortNotClosed = false;
            port.SendRequestEvent -= ProcessRequest;
            port.SendResponseEvent -= ProcessResponse;
            _bindedPortMAC = null;

            if (State != PortState.Close)
                State = PortState.Open;
            OnUnbindPortEvent(port);
        }
        public void Open()
        {
            if (State != PortState.Close) return;

            State = IsPortBinded ? PortState.Listened : PortState.Open;
        }
        public void Close()
        {
            if (State == PortState.Close) return;

            State = PortState.Close;
        }
        public void SendRequest(object sender, IRequest request)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (State == PortState.Close)
                throw new StateException($"{this} closed");


            if (State == PortState.Open) return;

            var senderMac = sender as string;
            if (senderMac == null || senderMac != _registeredTerminalMAC) return;

            if (!IsPortBinded)
            {
                var r = new DropedResponse(request, DropedResponse.PortUnbind);
                OnProcessResponseEvent(r);
                return;
            }

            if (!IsBindedPortNotClosed)
            {
                var r = new DropedResponse(request, DropedResponse.TargetPortUnplagged);
                OnProcessResponseEvent(r);
                return;
            }

            OnSendRequestEvent(SendRequestPreparation(request));
        }
        public void SendResponse(object sender, IResponse response)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (State == PortState.Close)
                throw new StateException($"{this} closed");


            if (State == PortState.Open) return;

            var senderMac = sender as string;
            if (senderMac == null || senderMac != _registeredTerminalMAC) return;

            if (!IsPortBinded)
            {
                var r = new DropedResponse(response.Request, DropedResponse.PortUnbind);
                OnProcessResponseEvent(r);
                return;
            }

            if (!IsBindedPortNotClosed)
            {
                var r = new DropedResponse(response.Request, DropedResponse.TargetPortUnplagged);
                OnProcessResponseEvent(r);
                return;
            }

            OnSendResponseEvent(SendResponsePreparation(response));
        }
        public virtual void ProcessRequest(object sender, IRequest request)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (State == PortState.Close)
                throw new StateException($"{this} closed");


            if (State == PortState.Open) return;

            var senderMac = sender as string;
            if (senderMac == null || senderMac != _bindedPortMAC) return;

            if (!IsTerminalRegistered)
            {
                var r = new DropedResponse(request, DropedResponse.TerminalNotRegistered);
                OnSendResponseEvent(r);
                return;
            }

            if (!IsRegisteredTerminalPluged)
            {
                var r = new DropedResponse(request, DropedResponse.TerminalUnplagged);
                OnSendResponseEvent(r);
                return;
            }

            OnProcessRequestEvent(ProcessRequestPreparation(request));
        }
        public virtual void ProcessResponse(object sender, IResponse response)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (State == PortState.Close)
                throw new StateException($"{this} closed");


            if (State == PortState.Open) return;

            var senderMac = sender as string;
            if (senderMac == null || senderMac != _bindedPortMAC) return;

            if (!IsTerminalRegistered)
            {
                var r = new DropedResponse(response.Request, DropedResponse.TerminalNotRegistered);
                OnSendResponseEvent(r);
                return;
            }

            if (!IsRegisteredTerminalPluged)
            {
                var r = new DropedResponse(response.Request, DropedResponse.TerminalUnplagged);
                OnSendResponseEvent(r);
                return;
            }

            OnProcessResponseEvent(ProcessResponsePreparation(response));
        }
        public virtual void ClearEvents()
        {
            StateChangingEvent = null;
            StateChangedEvent = null;
            RegisterTerminalEvent = null;
            RemoveTerminalEvent = null;
            BindPortEvent = null;
            UnbindPortEvent = null;
            ProcessRequestEvent = null;
            ProcessResponseEvent = null;
            SendRequestEvent = null;
            SendResponseEvent = null;
        }
        public override string ToString()
        {
            return $"Port[{base.ToString()}]";
        }

        protected virtual IRequest SendRequestPreparation(IRequest request)
        {
            return request;
        }
        protected virtual IResponse SendResponsePreparation(IResponse response)
        {
            return response;
        }
        protected virtual IRequest ProcessRequestPreparation(IRequest request)
        {
            return request;
        }
        protected virtual IResponse ProcessResponsePreparation(IResponse response)
        {
            return response;
        }

        private void OnStateChangingEvent(PortState newState)
        {
            StateChangingEvent?.Invoke(MACAddress, newState);
        }
        private void OnStateChangedEvent(PortState oldState)
        {
            StateChangedEvent?.Invoke(MACAddress, oldState, _state);
        }
        private void OnRegisterTerminalEvent(ITerminal terminal)
        {
            RegisterTerminalEvent?.Invoke(MACAddress, terminal);
        }
        private void OnRemoveTerminalEvent(ITerminal terminal)
        {
            RemoveTerminalEvent?.Invoke(MACAddress, terminal);
        }
        private void OnBindPortEvent(IPort port)
        {
            BindPortEvent?.Invoke(MACAddress, port);
        }
        private void OnUnbindPortEvent(IPort port)
        {
            UnbindPortEvent?.Invoke(MACAddress, port);
        }
        private void OnProcessRequestEvent(IRequest request)
        {
            ProcessRequestEvent?.Invoke(MACAddress, request);
        }
        private void OnProcessResponseEvent(IResponse response)
        {
            ProcessResponseEvent?.Invoke(MACAddress, response);
        }
        private void OnSendRequestEvent(IRequest request)
        {
            SendRequestEvent?.Invoke(MACAddress, request);
        }
        private void OnSendResponseEvent(IResponse response)
        {
            SendResponseEvent?.Invoke(MACAddress, response);
        }

        private void TerminalOnStateChangedEvent(object sender, TerminalState oldState, TerminalState newState)
        {
            IsRegisteredTerminalPluged = newState != TerminalState.Unplagged;
        }
        private void PortOnStateChangedEvent(object sender, PortState oldState, PortState newState)
        {
            IsBindedPortNotClosed = newState != PortState.Close;
        }
    }
}
