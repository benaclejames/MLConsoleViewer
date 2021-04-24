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

        private static void HandleMelonMsg(ConsoleColor melonColor, ConsoleColor txtColor, string callingMod, string logText)
        {
            if (callingMod != "MLConsoleViewer" || logText == "Hello!")
                new MelonLog(melonColor, txtColor, FindOrCreateModTracker(callingMod), logText);
        }

        private static void HandleWarningOrError(MelonLogType logType, string callingMod, string logText)
        {
            if (callingMod != "MLConsoleViewer" || logText == "Hello!")
                new MelonLog(FindOrCreateModTracker(callingMod), logText, logType);
        }

        private static ModTracker FindOrCreateModTracker(string modName)
        {
            if (modName == string.Empty) return null;
            return ModTracker.RegisteredMods.Find(mod => mod.ModName == modName) ?? new ModTracker(modName);
        }
    }
}