using System;
using System.Collections.Generic;
using Task3_АТS.ATS;
using Task3_АТS.ATS.Port;
using Task3_АТS.ATS.Terminal;
using Task3_АТS.BillingSystem;
using Task3_АТS.Test;

namespace Task3_АТS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConnectionDropedTest();

            // --- ConnectionDropedTest() console log ---

            //Station[AAA] : Terminal[A] maped to Port[AA]
            //Port[AA] : сменил состояние с Close на Open
            //Station[AAA] : Terminal[B] maped to Port[BB]
            //Port[BB] : сменил состояние с Close на Open
            //Station[AAA] : Terminal[C] maped to Port[CC]
            //Port[CC] : сменил состояние с Close на Open
            //Terminal[A] : сменил состояние с Unplagged на Offline
            //Terminal[B] : сменил состояние с Unplagged на Offline
            //Terminal[C] : сменил состояние с Unplagged на Offline
            //
            //Terminal[A] : сменил состояние с Offline на Online
            //Terminal[A] : попытка соединения по номеру 102
            //Port[AA] : сменил состояние с Open на Listened
            //Port[AA] : получил маршрут до Port[BB]
            //Port[BB] : сменил состояние с Open на Listened
            //Port[BB] : получил маршрут до Port[AA]
            //Port[AA] : передал по маршруту исходящий запрос
            //Port[BB] : передал по маршруту входящий запрос
            //Terminal[B] : сменил состояние с Offline на Online
            //Port[BB] : передал по маршруту исходящий ответ
            //Port[AA] : передал по маршруту входящий ответ
            //Terminal[A] : получил ответ -СОЕДИНЕНИЕ УСТАНОВЛЕНО
            //Terminal[A] : отправил сообщение -Hi!
            //Port[AA] : передал по маршруту исходящий запрос
            //Port[BB] : передал по маршруту входящий запрос
            //Terminal[B] : получил сообщение -Hi!
            //Terminal[B] : отправил сообщение -Hallo!
            //Port[BB] : передал по маршруту исходящий запрос
            //Port[AA] : передал по маршруту входящий запрос
            //Terminal[A] : получил сообщение -Hallo!
            //Terminal[C] : сменил состояние с Offline на Online
            //Terminal[C] : попытка соединения по номеру 102
            //Port[CC] : передал по маршруту входящий ответ
            //Terminal[C] : сменил состояние с Online на Offline
            //Terminal[C] : получил сообщение об ошибке -3332
            //Terminal[A] : отправил сообщение -How are you?!
            //Port[AA] : передал по маршруту исходящий запрос
            //Port[BB] : передал по маршруту входящий запрос
            //Terminal[B] : получил сообщение -How are you?!
            //Port[AA] : сменил состояние с Listened на Close
            //Terminal[B] : отправил сообщение -I am ok
            //Port[BB] : передал по маршруту входящий ответ
            //Terminal[B] : сменил состояние с Online на Offline
            //Terminal[B] : получил сообщение об ошибке -8887
            //Port[AA] : забыл маршрут до Port[BB]
            //Terminal[A] : сменил состояние с Online на Offline
            //Port[BB] : сменил состояние с Listened на Open
            //Port[BB] : забыл маршрут до Port[AA]
            //
            //Port[AA] : сменил состояние с Close на Open
            //Terminal[C] : сменил состояние с Offline на Online
            //Terminal[C] : попытка соединения по номеру 101
            //Port[CC] : сменил состояние с Open на Listened
            //Port[CC] : получил маршрут до Port[AA]
            //Port[AA] : сменил состояние с Open на Listened
            //Port[AA] : получил маршрут до Port[CC]
            //Port[CC] : передал по маршруту исходящий запрос
            //Port[AA] : передал по маршруту входящий запрос
            //Terminal[A] : сменил состояние с Offline на Online
            //Port[AA] : передал по маршруту исходящий ответ
            //Port[CC] : передал по маршруту входящий ответ
            //Terminal[C] : получил ответ -СОЕДИНЕНИЕ УСТАНОВЛЕНО
            //Terminal[A] : сменил состояние с Online на Unplagged
            //Terminal[C] : отправил сообщение -Hi
            //Port[CC] : передал по маршруту исходящий запрос
            //Port[AA] : передал по маршруту исходящий ответ
            //Port[CC] : передал по маршруту входящий ответ
            //Terminal[C] : сменил состояние с Online на Offline
            //Terminal[C] : получил сообщение об ошибке -1234
            //Port[CC] : сменил состояние с Listened на Open
            //Port[CC] : забыл маршрут до Port[AA]
            //Port[AA] : сменил состояние с Listened на Open
            //Port[AA] : забыл маршрут до Port[CC]

            BillingSystemTest();

            // --- BillingSystemTest() console log ---

            //Station[AAA] : Terminal[A] maped to Port[AA]
            //Port[AA] : сменил состояние с Close на Open
            //Station[AAA] : Terminal[B] maped to Port[BB]
            //Port[BB] : сменил состояние с Close на Open
            //Terminal[A] : сменил состояние с Unplagged на Offline
            //Terminal[B] : сменил состояние с Unplagged на Offline
            //------FIRST MONTH------
            //-- - First call-- -
            //Terminal[A] : сменил состояние с Offline на Online
            //Terminal[A] : попытка соединения по номеру 102
            //Port[AA] : сменил состояние с Open на Listened
            //Port[AA] : получил маршрут до Port[BB]
            //Port[BB] : сменил состояние с Open на Listened
            //Port[BB] : получил маршрут до Port[AA]
            //Port[AA] : передал по маршруту исходящий запрос
            //Port[BB] : передал по маршруту входящий запрос
            //Terminal[B] : сменил состояние с Offline на Online
            //Port[BB] : передал по маршруту исходящий ответ
            //Port[AA] : передал по маршруту входящий ответ
            //Terminal[A] : получил ответ -СОЕДИНЕНИЕ УСТАНОВЛЕНО
            //Terminal[B] : отправил сообщение -Ololololol
            //Port[BB] : передал по маршруту исходящий запрос
            //Port[AA] : передал по маршруту входящий запрос
            //Terminal[A] : получил сообщение -Ololololol
            //Terminal[B] : сменил состояние с Online на Offline
            //Terminal[B] : ПРЕРВАЛ СОЕДИНЕНИЕ
            //Port[AA] : сменил состояние с Listened на Open
            //Port[AA] : забыл маршрут до Port[BB]
            //Terminal[A] : сменил состояние с Online на Offline
            //Port[BB] : сменил состояние с Listened на Open
            //Port[BB] : забыл маршрут до Port[AA]
            // ---Second call-- -
            //Terminal[B] : сменил состояние с Offline на Unplagged
            //Terminal[A] : сменил состояние с Offline на Online
            //Terminal[A] : попытка соединения по номеру 102
            //Port[AA] : сменил состояние с Open на Listened
            //Port[AA] : получил маршрут до Port[BB]
            //Port[BB] : сменил состояние с Open на Listened
            //Port[BB] : получил маршрут до Port[AA]
            //Port[AA] : передал по маршруту исходящий запрос
            //Port[BB] : передал по маршруту исходящий ответ
            //Port[AA] : передал по маршруту входящий ответ
            //Terminal[A] : сменил состояние с Online на Offline
            //Terminal[A] : получил сообщение об ошибке -1234
            //Port[AA] : сменил состояние с Listened на Open
            //Port[AA] : забыл маршрут до Port[BB]
            //Port[BB] : сменил состояние с Listened на Open
            //Port[BB] : забыл маршрут до Port[AA]
            //Terminal[B] : сменил состояние с Unplagged на Offline
            // ---third call-- -
            //Terminal[B] : сменил состояние с Offline на Online
            //Terminal[B] : попытка соединения по номеру 101
            //Port[BB] : сменил состояние с Open на Listened
            //Port[BB] : получил маршрут до Port[AA]
            //Port[AA] : сменил состояние с Open на Listened
            //Port[AA] : получил маршрут до Port[BB]
            //Port[BB] : передал по маршруту исходящий запрос
            //Port[AA] : передал по маршруту входящий запрос
            //Terminal[A] : сменил состояние с Offline на Online
            //Port[AA] : передал по маршруту исходящий ответ
            //Port[BB] : передал по маршруту входящий ответ
            //Terminal[B] : получил ответ -СОЕДИНЕНИЕ УСТАНОВЛЕНО
            //Terminal[B] : отправил сообщение -Ololol
            //Port[BB] : передал по маршруту исходящий запрос
            //Port[AA] : передал по маршруту входящий запрос
            //Terminal[A] : получил сообщение -Ololol
            //Terminal[A] : сменил состояние с Online на Offline
            //Terminal[A] : ПРЕРВАЛ СОЕДИНЕНИЕ
            //Port[BB] : сменил состояние с Listened на Open
            //Port[BB] : забыл маршрут до Port[AA]
            //Terminal[B] : сменил состояние с Online на Offline
            //Port[AA] : сменил состояние с Listened на Open
            //Port[AA] : забыл маршрут до Port[BB]
            //---statistic-- -
            //Source - 101, Tartget - 102, Started - 15 - Nov - 15 23:04:21, Duration - 00:00:03
            //Source - 101, Tartget - 102, Connection faild time - 15 - Nov - 15 23:04:21
            //Source - 102, Tartget - 101, Started - 15 - Nov - 15 23:04:21, Duration - 00:09:29
            //Debt of 101 - -1000
            //Debt of 102 - -4500
            //------ SECOND MONTH ------
            //---statistic-- -
            //Debt of 101 - -1000
            //Debt of 102 - -4500
            //-- - 101 changing tariff ---
            // tariff changed successfully
            //---101 changing tariff for the second time-- -
            //tariff not changed

            Console.ReadKey();
        }

        private static void ConnectionDropedTest()
        {
            // INIT TEST

            var a = new TestTerminal("A", new PhoneNumber("101"));
            var b = new TestTerminal("B", new PhoneNumber("102"));
            var c = new TestTerminal("C", new PhoneNumber("103"));

            var aa = new TestPort("AA");
            var bb = new TestPort("BB");
            var cc = new TestPort("CC");

            var station = new TestStation("AAA", new List<ITerminal> { a, b, c },
                                          new List<IPort> { aa, bb, cc });

            // START TEST

            var t1 = station.GetPreparedTerminal() as TestTerminal;
            var t2 = station.GetPreparedTerminal() as TestTerminal;
            var t3 = station.GetPreparedTerminal() as TestTerminal;

            if (t1 == null || t2 == null || t3 == null) return;

            t1.Plug();
            t2.Plug();
            t3.Plug();

            Console.WriteLine("\n");
            t1.Connect(t2.PhoneNumber);
            t1.SendMessage("Hi!");
            t2.SendMessage("Hallo!");
            t3.Connect(t2.PhoneNumber); // port not bind because target port busy
            t1.SendMessage("How are you?!");
            aa.Close(); // close port of t1 close
            t2.SendMessage("I am ok"); // droped because aa closed
            Console.WriteLine("\n");
            aa.Open();
            t3.Connect(t1.PhoneNumber);
            t1.Unplug();
            t3.SendMessage("Hi"); // droped because t1 unpluged
        }
        private static void BillingSystemTest()
        {
            // INIT TEST

            var a = new TestTerminal("A", new PhoneNumber("101"));
            var b = new TestTerminal("B", new PhoneNumber("102"));

            var aa = new TestPort("AA");
            var bb = new TestPort("BB");

            var station = new TestStation("AAA", new List<ITerminal> { a, b },
                                          new List<IPort> { aa, bb });

            var billing = new TestBillingSystem();

            ITariff tariffFirst = new TestTariff(connectCost: 1000, minCost: 300);
            ITariff tariffSecond = new TestTariff(connectCost: 0, minCost: 500);

            var provider = new TestProvider(station, billing);

            // START TEST

            var t1 = provider.SignContract(tariffFirst) as TestTerminal;
            var t2 = provider.SignContract(tariffSecond) as TestTerminal;

            if (t1 == null || t2 == null) return;

            t1.Plug();
            t2.Plug();

            Console.WriteLine("------ FIRST MONTH ------");

            Console.WriteLine("--- First call ---");
            t1.Connect(t2.PhoneNumber);
            t2.SendMessage("Ololololol");
            t2.Disconnect();

            Console.WriteLine(" --- Second call ---");
            t2.Unplug();
            t1.Connect(t2.PhoneNumber);
            t2.Plug();

            Console.WriteLine(" --- third call ---");
            t2.Connect(t1.PhoneNumber);
            t2.SendMessage("Ololol");
            t1.Disconnect();

            Console.WriteLine("--- statistic ---");
            var calls = provider.GetStatisticForPeriod(t1.PhoneNumber);
            foreach (var call in calls)
                Console.WriteLine(call);
            Console.WriteLine($"Debt of {t1.PhoneNumber} - {provider.GetDebt(t1.PhoneNumber)}");
            Console.WriteLine($"Debt of {t2.PhoneNumber} - {provider.GetDebt(t2.PhoneNumber)}");

            billing.StartNewPeriod();
            Console.WriteLine("------ SECOND MONTH ------");

            Console.WriteLine("--- statistic ---");
            calls = provider.GetStatisticForPeriod(t1.PhoneNumber);
            foreach (var call in calls)
                Console.WriteLine(call);
            Console.WriteLine($"Debt of {t1.PhoneNumber} - {provider.GetDebt(t1.PhoneNumber)}");
            Console.WriteLine($"Debt of {t2.PhoneNumber} - {provider.GetDebt(t2.PhoneNumber)}");

            Console.WriteLine($"--- {t1.PhoneNumber} changing tariff ---");
            if (provider.TryChangeTariff(t1.PhoneNumber, tariffSecond))
                Console.WriteLine("tariff changed successfully");
            else
                Console.WriteLine("tariff not changed");

            Console.WriteLine($"--- {t1.PhoneNumber} changing tariff for the second time ---");
            if (provider.TryChangeTariff(t1.PhoneNumber, tariffFirst))
                Console.WriteLine("tariff changed successfully");
            else
                Console.WriteLine("tariff not changed");
        }
    }
}
