using System;
using System.Collections.Generic;
using MelonLoader;

namespace MelonViewer
{
    public class ModTracker
    {
        public static readonly List<ModTracker> RegisteredMods = new List<ModTracker>();
        public readonly List<MelonLog> AwaitingLogs = new List<MelonLog>();

        public readonly string ModName;
        private DateTime _lastLogTime;

        public bool TimedOut
        {
            get
            {
                var isSpamming = (DateTime.Now - _lastLogTime).Milliseconds < 250;
                return isSpamming;
            }
        }

        public ModTracker(string modName)
        {
            ModName = modName;
            RegisteredMods.Add(this);
        }

        public void OnLog(MelonLog log)
        {
            AwaitingLogs.Add(log);

            if (!TimedOut && InGameConsoleInterface.Singleton != null)
                PurgeAwaiting();
        }

        public void PurgeAwaiting()
        {
            foreach (var waitingLog in AwaitingLogs)
                InGameConsoleInterface.Singleton.AppendConsoleText(waitingLog);
            AwaitingLogs.Clear();
            
            _lastLogTime = DateTime.Now;
        }
    }
}