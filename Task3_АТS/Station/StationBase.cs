using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Task3_АТS.Port;
using Task3_АТS.Terminal;

namespace Task3_АТS.Station
{
    public abstract class StationBase : IStation
    {
        private readonly ICollection<CallInfo> _connectionCollection;
        private readonly ICollection<CallInfo> _callCollection;
        private readonly ICollection<TerminalPortPair> _terminalPortPairCollection;
        private readonly IDictionary<PhoneNumber, IPort> _portMapping;


        public event EventHandler<CallInfo> CallInfoPrepared;


        protected StationBase(ICollection<TerminalPortPair> terminalPortPairCollection)
           : this()
        {
            if (terminalPortPairCollection == null)
                throw new ArgumentNullException(nameof(terminalPortPairCollection));

            foreach (var pair in _terminalPortPairCollection)
                AddTerminal(pair);
        }
        protected StationBase()
        {
            _connectionCollection = new List<CallInfo>();
            _callCollection = new List<CallInfo>();
            _terminalPortPairCollection = new List<TerminalPortPair>();
            _portMapping = new Dictionary<PhoneNumber, IPort>();
        }

        public abstract void RegisterEventHandlersForTerminal(ITerminal terminal);
        public abstract void RegisterEventHandlersForPort(IPort port);
        public virtual void AddTerminal(ITerminal terminal, IPort port)
        {
            if (terminal == null) throw new ArgumentNullException(nameof(terminal));
            if (port == null) throw new ArgumentNullException(nameof(port));

            AddTerminal(new TerminalPortPair(terminal, port));
        }
        public virtual void AddTerminal(TerminalPortPair terminalPortPair)
        {
            _terminalPortPairCollection.Add(terminalPortPair);

            MapTerminalToPort(terminalPortPair);

            RegisterEventHandlersForTerminal(terminalPortPair.Terminal);
            RegisterEventHandlersForPort(terminalPortPair.Port);
        }

        protected virtual void OnCallInfoPrepared(object sender, CallInfo callInfo)
        {
            CallInfoPrepared?.Invoke(sender, callInfo);
        }
        protected virtual ITerminal GetTerminalByPhoneNumber(PhoneNumber phoneNumber)
        {
            return _terminalCollection.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        }
        protected virtual IPort GetPortByPhoneNumber(PhoneNumber phoneNumber)
        {
            return _portMapping[phoneNumber];
        }
        protected virtual void RegisterOutgoingRequest(Requests.OutgoingCallRequest request)
        {
            if ((request.Source != default(PhoneNumber) && request.Target != default(PhoneNumber)) &&
                (GetCallInfo(request.Source) == null && GetConnectionInfo(request.Source) == null))
            {
                var callInfo = new CallInfo()
                {
                    Source = request.Source,
                    Target = request.Target,
                    Started = DateTime.Now
                };

                ITerminal targetTerminal = GetTerminalByPhoneNumber(request.Target);
                IPort targetPort = GetPortByPhoneNumber(request.Target);

                if (targetPort.State == PortState.Free)
                {
                    _connectionCollection.Add(callInfo);
                    targetPort.State = PortState.Busy;
                    targetTerminal.IncomingRequestFrom(request.Source);
                }
                else
                {
                    OnCallInfoPrepared(this, callInfo);
                }
            }
        }
        protected virtual void MapTerminalToPort(TerminalPortPair terminalPortPair)
        {
            var terminal = terminalPortPair.Terminal;
            var port = terminalPortPair.Port;

            _portMapping.Add(terminal.PhoneNumber, port);

            port.RegisterEventHandlersForTerminal(terminal);
            terminal.RegisterEventHandlersForPort(port);
        }
        protected virtual void UnMapTerminalFromPort(TerminalPortPair terminalPortPair)
        {
            var terminal = terminalPortPair.Terminal;
            var port = terminalPortPair.Port;

            _portMapping.Remove(terminal.PhoneNumber);
            terminal.RemoveEventHandlersForPort(port);
            port.RemoveEventHandlersForTerminal(terminal);
        }
        protected List<CallInfo> GetConnectionInfo(PhoneNumber actor)
        {
            return _connectionCollection.Where(x => (x.Source == actor || x.Target == actor)).ToList();
        }
        protected List<CallInfo> GetCallInfo(PhoneNumber actor)
        {
            return _callCollection.Where(x => (x.Source == actor || x.Target == actor)).ToList();
        }

        protected void SetPortStateWhenConnectionInterrupted(PhoneNumber source, PhoneNumber target)
        {
            var sourcePort = GetPortByPhoneNumber(source);
            if (sourcePort != null)
            {
                sourcePort.State = PortState.Free;
            }

            var targetPort = GetPortByPhoneNumber(target);
            if (targetPort != null)
            {
                targetPort.State = PortState.Free;
            }
        }

        protected void InterruptConnection(CallInfo callInfo)
        {
            _callCollection.Remove(callInfo);
            SetPortStateWhenConnectionInterrupted(callInfo.Source, callInfo.Target);
            OnCallInfoPrepared(this, callInfo);
        }

        protected void InterruptActiveCall(CallInfo callInfo)
        {
            callInfo.Duration = DateTime.Now - callInfo.Started;
            _callCollection.Remove(callInfo);
            SetPortStateWhenConnectionInterrupted(callInfo.Source, callInfo.Target);
            OnCallInfoPrepared(this, callInfo);
        }

        public void OnIncomingCallRespond(object sender, Responds.Respond respond)
        {
            var registeredCallInfo = GetConnectionInfo(respond.Source);
            if (registeredCallInfo != null)
            {
                switch (respond.State)
                {
                    case Responds.RespondState.Drop:
                        {
                            InterruptConnection(registeredCallInfo);
                            break;
                        }
                    case Responds.RespondState.Accept:
                        {
                            MakeCallActive(registeredCallInfo);
                            break;
                        }
                }
            }
            else
            {
                CallInfo currentCallInfo = GetCallInfo(respond.Source);
                if (currentCallInfo != null)
                {
                    InterruptActiveCall(currentCallInfo);
                }
            }
        }

        protected void MakeCallActive(CallInfo callInfo)
        {
            _connectionCollection.Remove(callInfo);
            callInfo.Started = DateTime.Now;
            _callCollection.Add(callInfo);
        }

        public void ClearEvents()
        {
            CallInfoPrepared = null;
        }
    }
}
}
