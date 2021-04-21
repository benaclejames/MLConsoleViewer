using System;
using System.Collections.Generic;
using MelonLoader;

namespace MelonViewer
{
    public enum MelonLogType
    {
        Msg,
        Warning,
        Error
    }
    
    public static class MelonConsoleInterface
    {
        public static List<MelonLog> AwaitingLogs = new List<MelonLog>();
        
        public static void AttachDelegates()
        {
            MelonLogger.MsgCallbackHandler += HandleMelonMsg;
            MelonLogger.WarningCallbackHandler += (s, s1) => HandleWarningOrError(MelonLogType.Warning, s, s1);
            MelonLogger.ErrorCallbackHandler += (s, s1) => HandleWarningOrError(MelonLogType.Error, s, s1);
        }

        public static void CatchupAwaitingLogs()
        {
            if (AwaitingLogs.Count <= 0) return;
            
            // Log Em
            foreach (var log in AwaitingLogs)
                InGameConsoleInterface.Singleton.AppendConsoleText(log);
                
            // Clear Em
            AwaitingLogs.Clear();
        }
        
        private static void HandleMelonMsg(ConsoleColor melonColor, ConsoleColor txtColor, string callingMod, string logText)
        {
            if (callingMod != "MLConsoleViewer" || logText == "Hello!")
                HandleNullOrAppendNewLine(new MelonLog(melonColor, txtColor, callingMod, logText));
        }

        private static void HandleWarningOrError(MelonLogType logType, string callingMod, string logText)
        {
            if (callingMod != "MLConsoleViewer" || logText == "Hello!")
                HandleNullOrAppendNewLine(new MelonLog(callingMod, logText, logType));
        }

        private static void HandleNullOrAppendNewLine(MelonLog logLine)
        {
            if (InGameConsoleInterface.Singleton == null)
                AwaitingLogs.Add(logLine);
            else
            {
                CatchupAwaitingLogs();
                InGameConsoleInterface.Singleton.AppendConsoleText(logLine);
            }
        }
    }
}