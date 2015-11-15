using System;
using System.Collections.Generic;
using Task3_АТS.ATS;
using Task3_АТS.ATS.Port;
using Task3_АТS.ATS.Station;
using Task3_АТS.ATS.Terminal;
using Task3_АТS.Test;

namespace Task3_АТS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var a = new TestTerminal("A", new PhoneNumber("101"));
            var b = new TestTerminal("B", new PhoneNumber("102"));
            var c = new TestTerminal("C", new PhoneNumber("103"));

            var aa = new TestPort("AA");
            var bb = new TestPort("BB");
            var cc = new TestPort("CC");

            var station = new TestStation("AAA", new List<ITerminal> { a, b, c }, 
                                          new List<IPort> { aa, bb, cc });



            TestTerminal t1 = station.GetPreparedTerminal() as TestTerminal;
            TestTerminal t2 = station.GetPreparedTerminal() as TestTerminal;
            TestTerminal t3 = station.GetPreparedTerminal() as TestTerminal;

            if (t1 == null || t2 == null || t3 == null) return;

            t1.Plug();
            t2.Plug();
            t3.Plug();

            t1.Connect(t2.PhoneNumber);
            t1.Unplug();
            t2.SendMessage("Hallo!");
            t2.Disconnect();

            Console.ReadKey();
        }
    }
}
