using Task3_АТS.ATS.Port;
using Task3_АТS.ATS.Terminal;

namespace Task3_АТS.ATS
{
    public delegate void StateChangedHandler<T>(object sender, T oldState, T newState);

    public delegate void TerminalPortHandler(object sender, ITerminal terminal, IPort port);
}
