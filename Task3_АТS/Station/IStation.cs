using System;
using Task3_АТS.Port;
using Task3_АТS.Terminal;

namespace Task3_АТS.Station
{
    public class TerminalPortPair
    {
        public ITerminal Terminal;
        public IPort Port;


        public TerminalPortPair(ITerminal terminal, IPort port)
        {
            if (terminal == null) throw new ArgumentNullException(nameof(terminal));
            if (port == null) throw new ArgumentNullException(nameof(port));

            Terminal = terminal;
            Port = port;
        }
    }

    public interface IStation
    {
        void RegisterEventHandlersForTerminal(ITerminal terminal);
        void RegisterEventHandlersForPort(IPort port);
        void AddTerminal(ITerminal terminal, IPort port);
        void AddTerminal(TerminalPortPair terminalPortPair);
    }
}
