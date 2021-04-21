using System.Reflection;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace MelonViewer
{
    public class InGameConsoleInterface
    {
        public static InGameConsoleInterface Singleton;
        
        private Text consoleText;

        public InGameConsoleInterface(Transform menuCanvasTransform)
        {
            // Instantiate
            var bundle = AssetBundle.LoadFromMemory(ExtractAb());
            var menuPrefab = bundle.LoadAsset<GameObject>("MLConsoleViewer");
            var menuObject = Object.Instantiate(menuPrefab);
            
            // Fix Transforms
            menuObject.transform.parent = menuCanvasTransform;
            menuObject.transform.localPosition = Vector3.zero;
            menuObject.transform.localScale = Vector3.oneVector;
            menuObject.transform.localRotation = new Quaternion(0, 0, 0, 1);
            
            // Find Components
            consoleText = menuObject.transform.FindChild("Console/TextArea/MLConsole/Viewport/Content").GetComponent<Text>();
            
            // Unload Unused
            bundle.Unload(false);

            Singleton = this;
            
            MelonConsoleInterface.CatchupAwaitingLogs();
        }

        public void AppendConsoleText(MelonLog logLine)
        {
            if (!string.IsNullOrWhiteSpace(consoleText.text)) consoleText.text += "\n";
            consoleText.text += logLine.MakeConsoleString();
        }

        private static byte[] ExtractAb()
        {
            var a = Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream("MelonViewer.MLConsoleViewer"))
            {
                if (resFilestream == null) return null;
                var ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
        }
    }
}