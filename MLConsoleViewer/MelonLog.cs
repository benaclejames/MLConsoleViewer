using System;
using MelonLoader;

namespace MelonViewer
{
    public class MelonLog
    {
        private MelonLogType logType = MelonLogType.Msg;
        private string modName, logText;
        private ConsoleColor melonColor, txtColor;
        private DateTime logTime = DateTime.Now;

        private static ConsoleColor GetConsoleColorForLogType(MelonLogType logType) =>
            logType == MelonLogType.Warning ? ConsoleColor.Yellow : ConsoleColor.Red;
        
        public MelonLog(ConsoleColor melonColor, ConsoleColor txtColor, string callingMod, string logText)
        {
            modName = callingMod;
            this.logText = logText;

            this.melonColor = melonColor;
            this.txtColor = txtColor;
        }

        public MelonLog(string callingMod, string logText, MelonLogType logType)
        {
            this.logType = logType;

            modName = callingMod;
            this.logText = $"[{logType.ToString().ToUpper()}] " + logText;

            melonColor = GetConsoleColorForLogType(logType);
            txtColor = GetConsoleColorForLogType(logType);
        }

        public string MakeConsoleString() => logType == MelonLogType.Msg ? $"[<color=green>{MakeTimestamp(logTime)}</color>] [<color={melonColor.ToString()}>{modName}</color>] <color={txtColor.ToString()}>{logText}</color>" : 
                $"<color={GetConsoleColorForLogType(logType)}>[{MakeTimestamp(logTime)}] [{modName}] {logText}</color>";

        private static string MakeTimestamp(DateTime time) => time.AddMilliseconds(-1).ToString("HH:mm:ss.fff");   // More often than not, the log callback is 1ms late
    }
}