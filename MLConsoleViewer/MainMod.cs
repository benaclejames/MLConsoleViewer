using System;
using System.Collections.Generic;
using MelonLoader;
using MelonViewer;
using MelonViewer.QuickMenu;
using UnityEngine;

[assembly: MelonInfo(typeof(MainMod), "MLConsoleViewer", "1.0.0", "benaclejames")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace MelonViewer
{
    public class MainMod : MelonMod
    {
        public enum LogType
        {
            Msg,
            Warning,
            Error
        }
        
        public static List<(LogType, ConsoleColor, ConsoleColor, string, string, DateTime)> ConsoleLogCache =
            new List<(LogType, ConsoleColor, ConsoleColor, string, string, DateTime)>();
        
        public override void OnSceneWasLoaded(int level, string levelName)
        {
            if (level == -1 && !QuickModeMenu.HasInitMenu)
                QuickModeMenu.InitializeMenu();
        }

        public override void OnApplicationStart()
        {
            MelonLogger.MsgCallbackHandler += AddMsgToCache;
            MelonLogger.WarningCallbackHandler += AddWarningToCache;
            MelonLogger.ErrorCallbackHandler += AddErrorToCache;
        }

        public static void AddMsgToCache(ConsoleColor melonColor, ConsoleColor txtColor, string callingMod,
            string logText) => ConsoleLogCache.Add((LogType.Msg, melonColor, txtColor, callingMod, logText, DateTime.Now));
        public static void AddWarningToCache(string callingMod, string logText) => 
            ConsoleLogCache.Add((LogType.Warning, ConsoleColor.Yellow, ConsoleColor.Yellow, callingMod, logText, DateTime.Now));
        public static void AddErrorToCache(string callingMod, string logText) => 
            ConsoleLogCache.Add((LogType.Error, ConsoleColor.Red, ConsoleColor.Red, callingMod, logText, DateTime.Now));
    }
}