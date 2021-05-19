using System;
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
        public static void AttachDelegates()
        {
            MelonLogger.MsgCallbackHandler += HandleMelonMsg;
            MelonLogger.WarningCallbackHandler += (s, s1) => HandleWarningOrError(MelonLogType.Warning, s, s1);
            MelonLogger.ErrorCallbackHandler += (s, s1) => HandleWarningOrError(MelonLogType.Error, s, s1);
        }

        public static void HandleMelonMsg(ConsoleColor melonColor, ConsoleColor txtColor, string callingMod, string logText)
        {
            //if (callingMod == "MLConsoleViewer") return;
            
            LogTracker.OnLog(new MelonLog(melonColor, txtColor, callingMod, logText));
        }

        public static void HandleWarningOrError(MelonLogType logType, string callingMod, string logText)
        {
            if (callingMod == "MLConsoleViewer") return;
            
            LogTracker.OnLog(new MelonLog(callingMod, logText, logType));
        }
    }
}