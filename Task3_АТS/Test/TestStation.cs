﻿using System;
using System.Collections.Generic;
using Task3_АТS.ATS.Port;
using Task3_АТS.ATS.Station;
using Task3_АТS.ATS.Terminal;

namespace Task3_АТS.Test
{
    public class TestStation : Station
    {
        public TestStation(string macAddress) 
            : base(macAddress)
        {
            StartLog();
        }
        public TestStation(string macAddress, ICollection<ITerminal> terminals, ICollection<IPort> ports)
            : base(macAddress, terminals, ports)
        {
            StartLog();
        }

        private void StartLog()
        {
            TerminalToPortMapedEvent += (sender, terminal, port) =>
            { Console.WriteLine($"{this} : {terminal} maped to {port}"); };
        }
    }
}
