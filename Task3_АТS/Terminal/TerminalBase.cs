using System.Collections.Generic;
using Task3_АТS.Port;

namespace Task3_АТS.Terminal
{
    public abstract class TerminalBase : ITerminal
    {
        private ICollection<ITerminal> _terminalCollection;
        private ICollection<IPort> _portCollection;
        private IDictionary<PhoneNumber, IPort> _portMapping;
    }
}
