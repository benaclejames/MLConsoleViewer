using UnityEngine;

namespace MelonViewer
{
    public class InGameConsoleInterface
    {
        public static InGameConsoleInterface Singleton;
        
        public readonly TabBadge NotificationTab;
        private readonly GameObject _menuParentRoot;
        private readonly Transform _contentTransform;
        private LogObject _latestLogObject;

        public InGameConsoleInterface(Transform menuCanvasTransform, GameObject notificationTab, AssetBundle bundle)
        {
            Singleton = this;
            _menuParentRoot = menuCanvasTransform.gameObject;
            
            // Instantiate
            var menuPrefab = bundle.LoadAsset<GameObject>("MLConsoleViewer");
            var menuObject = Object.Instantiate(menuPrefab);

            // Fix Transforms
            menuObject.transform.parent = menuCanvasTransform;
            menuObject.transform.localPosition = Vector3.zero;
            menuObject.transform.localScale = Vector3.oneVector;
            menuObject.transform.localRotation = new Quaternion(0, 0, 0, 1);
            
            // Find Components
            _contentTransform = menuObject.transform.Find("Console/TextArea/MLConsole/Viewport/Content");
            LogObject.ConsoleTextPrefab = bundle.LoadAsset<GameObject>("TextElement");
            LogObject.ConsoleTextPrefab.transform.parent = menuObject.transform;
            LogObject.ConsoleTextPrefab.active = false;
            _latestLogObject = new LogObject(_contentTransform);

            // Unload Unused
            NotificationTab = new TabBadge(notificationTab);
        }
        
        public void AppendConsoleText(MelonLog logLine)
        {
            if (_latestLogObject == null || !_latestLogObject.AppendText(logLine))
                _latestLogObject = new LogObject(_contentTransform);

            if (!_menuParentRoot.active)
                NotificationTab.NotifyNewLog(logLine);
        }
    }
}