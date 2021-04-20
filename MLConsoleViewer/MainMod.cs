using System;
using System.Collections.Generic;
using MelonLoader;
using MelonViewer;
using MelonViewer.QuickMenu;
using UnityEngine;

[assembly: MelonInfo(typeof(MainMod), "MLConsoleViewer", "1.0", "benaclejames")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace MelonViewer
{
    public class MainMod : MelonMod
    {
        public static List<(ConsoleColor, ConsoleColor, string, string, DateTime)> ConsoleLogCache =
            new List<(ConsoleColor, ConsoleColor, string, string, DateTime)>();
        
        public override void OnSceneWasLoaded(int level, string levelName)
        {
            if (level == -1 && !QuickModeMenu.HasInitMenu)
                QuickModeMenu.InitializeMenu();
        }

        public override void OnApplicationStart() => MelonLogger.MsgCallbackHandler += AddLogToCache;
        

        public static void AddLogToCache(ConsoleColor melonColor, ConsoleColor txtColor, string callingMod,
            string logText) => ConsoleLogCache.Add((melonColor, txtColor, callingMod, logText, DateTime.Now));
    }
}