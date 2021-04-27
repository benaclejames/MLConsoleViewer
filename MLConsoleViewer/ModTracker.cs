using System.Collections.Generic;

namespace MelonViewer
{
    public class ModTracker
    {
        public static readonly List<ModTracker> RegisteredMods = new List<ModTracker>();
        private static readonly List<MelonLog> AwaitingLogs = new List<MelonLog>();

        public readonly string ModName;

        public ModTracker(string modName)
        {
            ModName = modName;
            RegisteredMods.Add(this); 
        }

        public static void OnLog(MelonLog log) => AwaitingLogs.Add(log);

        public static void PurgeAwaiting()
        {
            foreach (var waitingLog in AwaitingLogs)
                InGameConsoleInterface.Singleton.AppendConsoleText(waitingLog);
            AwaitingLogs.Clear();
        }
    }
}