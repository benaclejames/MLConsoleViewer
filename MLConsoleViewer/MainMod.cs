using System.Collections;
using MelonLoader;
using MelonViewer;
using UnityEngine;

[assembly: MelonInfo(typeof(MainMod), "MLConsoleViewer", "1.1.1", "benaclejames")]
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

        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(UpdatePendingLogs());
            MelonConsoleInterface.AttachDelegates();
        }

        private IEnumerator UpdatePendingLogs()
        {
            for (;;)
            {
                if (InGameConsoleInterface.Singleton != null)
                    LogTracker.PurgeAwaiting(InGameConsoleInterface.Singleton);
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}