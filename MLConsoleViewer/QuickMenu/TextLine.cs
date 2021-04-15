using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MelonViewer.QuickMenu
{
    public class TextLine
    {
        public TextLine(Transform parent)
        {
            var fonts = Resources.FindObjectsOfTypeAll<Font>();
            var font = fonts.First(source => source.name == "Dosis-Regular");

            var newObj = new GameObject("TestText");
            newObj.transform.SetParent(parent);
            newObj.transform.localPosition = Vector3.zeroVector;
            newObj.transform.localScale = Vector3.one;
            var newTxt = newObj.AddComponent<Text>();
            var sizeFitter = newObj.AddComponent<ContentSizeFitter>();
            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            newTxt.font = font;
            newTxt.fontSize = 50;
            newTxt.text = "<color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> <color=red>HUG</color> <color=green>HUG</color> ";
            newObj.transform.localRotation = Quaternion.Euler(Vector3.zeroVector);
            newObj.transform.localPosition = Vector3.zeroVector;

            var rectTransform = newObj.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -10);
            rectTransform.offsetMax = new Vector2(1096, -10);
        }
    }
}