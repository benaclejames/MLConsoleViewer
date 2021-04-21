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
        public override void OnSceneWasLoaded(int level, string levelName)
        {
            if (level == -1 && !QuickModeMenu.HasInitMenu)
                QuickModeMenu.InitializeMenu();
        }

        public override void OnApplicationStart() => MelonConsoleInterface.AttachDelegates();

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                MelonLogger.Msg("Hello!");
                MelonLogger.Warning("Hello!");
                MelonLogger.Error("Hello!");
            }
        }
    }
}