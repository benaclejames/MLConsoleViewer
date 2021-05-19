using System.Collections.Generic;

namespace MelonViewer
{
    public static class LogTracker
    {
        private static readonly List<MelonLog> AwaitingLogs = new List<MelonLog>();
        public static void OnLog(MelonLog log) => AwaitingLogs.Add(log);

        public static void PurgeAwaiting(InGameConsoleInterface consoleInterface)
        {
            foreach (var waitingLog in AwaitingLogs)
                consoleInterface.AppendConsoleText(waitingLog);
            AwaitingLogs.Clear();
        }
    }
}