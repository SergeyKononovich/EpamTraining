using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Task4_FolderMonitor.BL.Utils
{
    public class Utilities
    {
        public static void TrySeveralTimesIfException(uint numTries, int timeSleepInMs, Action<int> action,
                                                      List<Type> ignoredExceptions = null)
        {
            bool ignoreAll = ignoredExceptions == null || ignoredExceptions.Count == 0;
            if (!ignoreAll && ignoredExceptions.Any(type => !type.IsSubclassOf(typeof(Exception))))
                throw new ArgumentException("Elements must be subclass of 'Exception'", nameof(ignoredExceptions));
            if (action == null) throw new ArgumentNullException(nameof(action));

            int tries = 0;
            while (true)
            {
                try
                {
                    ++tries;
                    action(tries);

                    break;
                }
                catch (Exception e)
                {
                    if (ignoreAll || ignoredExceptions.Any(ignored => e.GetType() == ignored))
                    {
                        if (tries > numTries) throw;

                        // Wait and retry
                        Thread.Sleep(timeSleepInMs);
                    }
                    else throw;
                }
            }
        }
    }
}