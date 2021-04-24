using System;

namespace MelonViewer
{
    public class MelonLog
    {
        public readonly MelonLogType LogType = MelonLogType.Msg;
        public readonly ModTracker OriginatingMod;
        private readonly string _logText;
        private readonly ConsoleColor _melonColor, _txtColor;
        private readonly DateTime _logTime = DateTime.Now;

        private static ConsoleColor GetConsoleColorForLogType(MelonLogType logType) =>
            logType == MelonLogType.Warning ? ConsoleColor.Yellow : ConsoleColor.Red;
        
        public MelonLog(ConsoleColor melonColor, ConsoleColor txtColor, ModTracker parentMod, string logText)
        {
            OriginatingMod = parentMod;
            _logText = logText;

            _melonColor = melonColor;
            _txtColor = txtColor;
            
            OriginatingMod.OnLog(this);
        }

        public MelonLog(ModTracker parentMod, string logText, MelonLogType logType)
        {
            OriginatingMod = parentMod;
            LogType = logType;
            
            _logText = $"[{logType.ToString().ToUpper()}] " + logText;

            _melonColor = GetConsoleColorForLogType(logType);
            _txtColor = GetConsoleColorForLogType(logType);
            
            OriginatingMod.OnLog(this);
        }

        public string MakeConsoleString() => LogType == MelonLogType.Msg ? $"[<color=green>{MakeTimestamp(_logTime)}</color>] [<color={_melonColor.ToString()}>{OriginatingMod.ModName}</color>] <color={_txtColor.ToString()}>{_logText}</color>" : 
                $"<color={GetConsoleColorForLogType(LogType)}>[{MakeTimestamp(_logTime)}] [{OriginatingMod.ModName}] {_logText}</color>";

        private static string MakeTimestamp(DateTime time) => time.AddMilliseconds(-1).ToString("HH:mm:ss.fff");   // More often than not, the log callback is 1ms late
    }
}