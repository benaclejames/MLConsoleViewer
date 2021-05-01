using UnityEngine;
using UnityEngine.UI;

namespace MelonViewer
{
    public class InGameConsoleInterface
    {
        public static InGameConsoleInterface Singleton;
        
        private readonly GameObject _consoleTextComponent;
        public readonly TabBadge NotificationTab;
        private readonly GameObject _menuParentRoot;
        private readonly Transform _contentTransform;

        public InGameConsoleInterface(Transform menuCanvasTransform, GameObject notificationTab, AssetBundle bundle)
        {
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
            _consoleTextComponent = bundle.LoadAsset<GameObject>("TextElement");
            _consoleTextComponent.transform.parent = menuObject.transform;
            _consoleTextComponent.active = false;
            
            // Unload Unused
            NotificationTab = new TabBadge(notificationTab);

            Singleton = this;
            
            
            LogTracker.PurgeAwaiting();
        }
        
        public void AppendConsoleText(MelonLog logLine)
        {
            if (_consoleTextComponent == null) return;

            var newText = Object.Instantiate(_consoleTextComponent);
            newText.transform.parent = _contentTransform;
            newText.transform.localPosition = Vector3.zero;
            newText.transform.localScale = Vector3.oneVector;
            newText.transform.localRotation = new Quaternion(0, 0, 0, 1);
            newText.active = true;
            
            newText.GetComponent<Text>().text = logLine.MakeConsoleString();
            
            if (!_menuParentRoot.active)
                NotificationTab.NotifyNewLog(logLine);
        }
    }
}