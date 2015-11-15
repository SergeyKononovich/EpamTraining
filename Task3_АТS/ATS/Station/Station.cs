using System;
using System.Collections.Generic;
using System.Linq;
using Task3_АТS.ATS.NetworkEntity;
using Task3_АТS.ATS.Port;
using Task3_АТS.ATS.Request;
using Task3_АТS.ATS.Response;
using Task3_АТS.ATS.Terminal;

namespace Task3_АТS.ATS.Station
{
    public class Station : NetworkEntityBase, IStation
    {
        protected struct SourseTargetPair
        {
            public PhoneNumber SourceNumber { get; }
            public PhoneNumber TargetNumber { get; }


            public SourseTargetPair(PhoneNumber sourceNumber, PhoneNumber targetNumber)
            {
                SourceNumber = sourceNumber;
                TargetNumber = targetNumber;
            }
        }

        
        private readonly ICollection<ITerminal> _terminals;
        private readonly ICollection<IPort> _ports;
        private readonly IDictionary<SourseTargetPair, CallInfo> _activeConnections;
        private readonly IDictionary<SourseTargetPair, CallInfo> _activeCalls;
        private readonly IDictionary<PhoneNumber, IPort> _portMapping;


        public event TerminalPortHandler TerminalToPortMapedEvent;
        public event EventHandler<CallInfo> ConnectionFailedEvent;
        public event EventHandler<CallInfo> CallStartEvent;
        public event EventHandler<CallInfo> CallEndEvent;


        public Station(string macAddress, ICollection<ITerminal> terminals, ICollection<IPort> ports)
           : this(macAddress)
        {
            if (terminals == null) throw new ArgumentNullException(nameof(terminals));
            if (ports == null) throw new ArgumentNullException(nameof(ports));

            foreach (var t in terminals)
                if (t != null) AddTerminal(t);

            foreach (var p in ports)
                if (p != null) AddPort(p);
        }
        public Station(string macAddress) 
            : base(macAddress)
        {
            _terminals = new List<ITerminal>();
            _ports = new List<IPort>();
            _activeConnections = new Dictionary<SourseTargetPair, CallInfo>();
            _activeCalls =new Dictionary<SourseTargetPair, CallInfo>();
            _portMapping = new Dictionary<PhoneNumber, IPort>();
        }

        public void AddTerminal(ITerminal terminal)
        {
            if (terminal == null) throw new ArgumentNullException(nameof(terminal));


            RegisterTerminal(terminal);
            _terminals.Add(terminal);
        }
        public void AddPort(IPort port)
        {
            if (port == null) throw new ArgumentNullException(nameof(port));


            RegisterPort(port);
            _ports.Add(port);
        }
        public virtual ITerminal GetPreparedTerminal()
        {
            IPort freePort = _ports.FirstOrDefault(p => p.IsTerminalRegistered == false);
            ITerminal freeTerminal = _terminals.FirstOrDefault(t => t.IsPortRegistered == false);

            if (freePort != null && freeTerminal != null)
            {
                MapTerminalToPort(freeTerminal, freePort);
                freePort.Open();

                return freeTerminal;
            }

            return null;
        }
        public virtual void ClearEvents()
        {
            ConnectionFailedEvent = null;
            CallStartEvent = null;
            CallEndEvent = null;
        }
        public override string ToString()
        {
            return $"Station[{base.ToString()}]";
        }

        protected virtual void RegisterTerminal(ITerminal terminal)
        {
            if (terminal == null) throw new ArgumentNullException(nameof(terminal));


            terminal.ConnectingEvent += TerminalOnConnectingEvent;
            terminal.ConnectSucceededEvent += TerminalOnConnectSucceededEvent;
            terminal.ConnectFailedEvent += TerminalOnConnectFailedEvent;
            terminal.DropedEvent += TerminalOnDropedEvent;
            terminal.DisconnectEvent += TerminalOnDisconnectEvent;
        }
        protected virtual void RegisterPort(IPort port)
        {
        }
        protected virtual void MapTerminalToPort(ITerminal terminal, IPort port)
        {
            port.RegisterTerminal(terminal);
            terminal.RegisterPort(port);
            
            _portMapping.Add(terminal.PhoneNumber, port);
            OnTerminalToPortMapedEvent(terminal, port);
        }
        protected IPort GetPortByPhoneNumber(PhoneNumber phoneNumber)
        {
            IPort port;
            _portMapping.TryGetValue(phoneNumber, out port);

            return port;
        }
        protected CallInfo GetConnectionByActor(PhoneNumber actor)
        {
            return _activeConnections.FirstOrDefault(p => p.Key.SourceNumber == actor ||
                                                     p.Key.TargetNumber == actor).Value;

        }
        protected CallInfo GetCallByActor(PhoneNumber actor)
        {
            return _activeCalls.FirstOrDefault(p => p.Key.SourceNumber == actor || 
                                               p.Key.TargetNumber == actor).Value;

        }
        protected CallInfo RemoveActiveConnectionByActor(PhoneNumber actor)
        {
            CallInfo call = GetConnectionByActor(actor);
            if (call != null && _activeConnections.Remove(new SourseTargetPair(call.Source, call.Target)))
                return call;

            return null;
        }
        protected CallInfo RemoveActiveCallByActor(PhoneNumber actor)
        {
            CallInfo call = GetCallByActor(actor);
            if (call != null && _activeCalls.Remove(new SourseTargetPair(call.Source, call.Target)))
                return call;

            return null;
        }
        protected void UnbindPortsByCall(CallInfo call)
        {
            IPort sourcePort = GetPortByPhoneNumber(call.Source);
            IPort targetPort = GetPortByPhoneNumber(call.Target);

            if (sourcePort != null && sourcePort.IsPortBinded &&
                targetPort != null && targetPort.IsPortBinded)
            {
                sourcePort.UnbindPort(targetPort);
                targetPort.UnbindPort(sourcePort);
            }
        }

        private void OnTerminalToPortMapedEvent(ITerminal terminal, IPort port)
        {
            TerminalToPortMapedEvent?.Invoke(MACAddress, terminal, port);
        }
        private void OnConnectionFailedEvent(CallInfo e)
        {
            ConnectionFailedEvent?.Invoke(MACAddress, e);
        }
        private void OnCallStartEvent(CallInfo e)
        {
            CallStartEvent?.Invoke(MACAddress, e);
        }
        private void OnCallEndEvent(CallInfo e)
        {
            CallEndEvent?.Invoke(MACAddress, e);
        }

        private void TerminalOnConnectingEvent(object sender, ConnectRequest connectRequest)
        {
            if (connectRequest == null) throw new ArgumentNullException(nameof(connectRequest));


            IPort sourcePort = GetPortByPhoneNumber(connectRequest.Sender);
            IPort targetPort = GetPortByPhoneNumber(connectRequest.Target);

            if (sourcePort != null && sourcePort.State == PortState.Open &&
                targetPort != null && targetPort.State == PortState.Open)
            {
                sourcePort.BindPort(targetPort);
                targetPort.BindPort(sourcePort);
            }

            CallInfo call = new CallInfo
            {
                Source = connectRequest.Sender,
                Target = connectRequest.Target,
                Started = DateTime.Now
            };
            _activeConnections.Add(new SourseTargetPair(connectRequest.Sender, connectRequest.Target), call);
        }
        private void TerminalOnConnectSucceededEvent(object sender, ConnectResponse connectResponse)
        {
            if (connectResponse == null) throw new ArgumentNullException(nameof(connectResponse));


            CallInfo call = RemoveActiveConnectionByActor(connectResponse.ConnectRequest.Sender);
            if (call != null)
            {
                _activeCalls.Add(new SourseTargetPair(call.Source, call.Target), call);
                OnCallStartEvent(call);
            }
        }
        private void TerminalOnConnectFailedEvent(object sender, ConnectResponse connectResponse)
        {
            if (connectResponse == null) throw new ArgumentNullException(nameof(connectResponse));


            CallInfo call = RemoveActiveConnectionByActor(connectResponse.ConnectRequest.Sender);
            if (call != null)
            {
                OnConnectionFailedEvent(call);
                UnbindPortsByCall(call);
            }
        }
        private void TerminalOnDropedEvent(object sender, DropedResponse dropedResponse)
        {
            CallInfo call = RemoveActiveConnectionByActor(dropedResponse.Request.Sender);
            if (call != null)
            {
                OnConnectionFailedEvent(call);
                UnbindPortsByCall(call);
                return;
            }

            call = RemoveActiveCallByActor(dropedResponse.Request.Sender);
            if (call != null)
            {
                Random r = new Random();
                call.Duration = TimeSpan.FromSeconds(r.Next(0, 600));
                OnCallEndEvent(call);
                UnbindPortsByCall(call);
            }
        }
        private void TerminalOnDisconnectEvent(object sender, PhoneNumber phoneNumber)
        {
            CallInfo call = RemoveActiveCallByActor(phoneNumber);
            if (call != null)
            {
                Random r = new Random();
                call.Duration = TimeSpan.FromSeconds(r.Next(0, 600));
                OnCallEndEvent(call);
                UnbindPortsByCall(call);
            }
        }
    }
}
