using UnityEngine;
using UnityEngine.UI;

namespace MelonViewer
{
    public class LogObject
    {
        public static GameObject ConsoleTextPrefab;
        private readonly Text _textComponent;
        
        public LogObject(Transform parent)
        {
            var newText = Object.Instantiate(ConsoleTextPrefab);
            newText.transform.parent = parent;
            newText.transform.localPosition = Vector3.zero;
            newText.transform.localScale = Vector3.oneVector;
            newText.transform.localRotation = new Quaternion(0, 0, 0, 1);
            newText.active = true;

            _textComponent = newText.GetComponent<Text>();
            _textComponent.text = "";
        }

        public bool AppendText(MelonLog logLine)
        {
            if (ConsoleTextPrefab == null || _textComponent == null || _textComponent.text.Length > 50000)
                return false;

            if (_textComponent.text.Length > 0) _textComponent.text += "\n";    // Only add a newline when there's text above
            
            _textComponent.text += logLine;
            return true;
        }
    }
}