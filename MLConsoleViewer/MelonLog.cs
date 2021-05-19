using System;

namespace MelonViewer
{
    public class MelonLog
    {
        public readonly MelonLogType LogType = MelonLogType.Msg;
        private readonly string _originatingMod;
        private readonly string _logText;
        private readonly ConsoleColor _melonColor, _txtColor;
        private readonly DateTime _logTime = DateTime.Now;
        public static implicit operator string(MelonLog log) => log.MakeConsoleString();

        private static ConsoleColor GetConsoleColorForLogType(MelonLogType logType) =>
            logType == MelonLogType.Warning ? ConsoleColor.Yellow : ConsoleColor.Red;
        
        public MelonLog(ConsoleColor melonColor, ConsoleColor txtColor, string parentMod, string logText)
        {
            _originatingMod = parentMod;
            _logText = logText;

            _melonColor = melonColor;
            _txtColor = txtColor;
        }

        public MelonLog(string parentMod, string logText, MelonLogType logType)
        {
            _originatingMod = parentMod;
            LogType = logType;
            
            _logText = $"[{logType.ToString().ToUpper()}] " + logText;

            _melonColor = GetConsoleColorForLogType(logType);
            _txtColor = GetConsoleColorForLogType(logType);
        }

        private string MakeConsoleString() => LogType == MelonLogType.Msg ? $"[<color=green>{MakeTimestamp(_logTime)}</color>] [<color={_melonColor.ToString()}>{_originatingMod}</color>] <color={_txtColor.ToString()}>{_logText}</color>" : 
                $"<color={GetConsoleColorForLogType(LogType)}>[{MakeTimestamp(_logTime)}] [{_originatingMod}] {_logText}</color>";

        private static string MakeTimestamp(DateTime time) => time.AddMilliseconds(-1).ToString("HH:mm:ss.fff");   // More often than not, the log callback is 1ms late
    }
}