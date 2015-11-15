using System;
using Task3_АТS.ATS.NetworkEntity;
using Task3_АТS.ATS.Port;
using Task3_АТS.ATS.Terminal;

namespace Task3_АТS.ATS.Station
{
    public interface IStation : INetworkEntity
    {
        event EventHandler<CallInfo> ConnectionFailedEvent;
        event EventHandler<CallInfo> CallStartEvent;
        event EventHandler<CallInfo> CallEndEvent;


        void AddTerminal(ITerminal terminal);
        void AddPort(IPort port);
        ITerminal GetPreparedTerminal();
        void ClearEvents();
    }
}
