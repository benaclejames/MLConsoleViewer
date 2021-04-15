using System;
using System.Collections.Generic;
using System.Linq;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MelonViewer.QuickMenu
{
    public static class QuickModeMenu
    {
        public static bool HasInitMenu;

        private static string melonIcon =
            "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";

        private static string melonConsoleIcon =
            "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";

        public static void InitializeMenu()
        {
            CreateNotificationTab("MLConsoleViewer", "Text", Color.magenta, // Organization 100
                melonConsoleIcon);
            HasInitMenu = true;
        }

        private static (Transform tab, Transform menu) CreateNotificationTab(string name, string text, Color color, string imageDataBase64 = null)
        {
            List<GameObject> existingTabs = Resources.FindObjectsOfTypeAll<MonoBehaviourPublicObCoGaCoObCoObCoUnique>()[0].field_Public_ArrayOf_GameObject_0.ToList();

            global::QuickMenu quickMenu = Resources.FindObjectsOfTypeAll<global::QuickMenu>()[0];

            // Tab

            MonoBehaviourPublicObCoGaCoObCoObCoUnique quickModeTabs = quickMenu.transform.Find("QuickModeTabs").GetComponent<MonoBehaviourPublicObCoGaCoObCoObCoUnique>();
            Transform newTab = GameObject.Instantiate(quickModeTabs.transform.Find("NotificationsTab"), quickModeTabs.transform);
            newTab.name = name;
            GameObject.DestroyImmediate(newTab.GetComponent<MonoBehaviourPublicGaTeSiSiUnique>());
            SetTabIndex(newTab, (MonoBehaviourPublicObCoGaCoObCoObCoUnique.EnumNPublicSealedvaHoNoPl4vUnique)existingTabs.Count);
            newTab.Find("Badge").GetComponent<RawImage>().color = color;
            newTab.Find("Badge/NotificationsText").GetComponent<Text>().text = text;

            existingTabs.Add(newTab.gameObject);
 
            Resources.FindObjectsOfTypeAll<MonoBehaviourPublicObCoGaCoObCoObCoUnique>()[0].field_Public_ArrayOf_GameObject_0 = existingTabs.ToArray();

            if (imageDataBase64 != null)
                newTab.Find("Icon").GetComponent<Image>().sprite = CreateSpriteFromBase64(imageDataBase64);
            else
                newTab.Find("Icon").gameObject.SetActive(false);

            // Menu

            Transform quickModeMenus = quickMenu.transform.Find("QuickModeMenus");
            RectTransform newMenu = new GameObject(name + "Menu", new Il2CppSystem.Type[] { Il2CppType.Of<RectTransform>() }).GetComponent<RectTransform>();
            newMenu.SetParent(quickModeMenus, false);
            newMenu.anchorMin = new Vector2(0, 1);
            newMenu.anchorMax = new Vector2(0, 1);
            newMenu.sizeDelta = new Vector2(1680f, 1200f);
            newMenu.pivot = new Vector2(0.5f, 0.5f);
            newMenu.anchoredPosition = new Vector2(0, 200f);
            newMenu.gameObject.SetActive(false);
            

            // Tab interaction
            var tabButton = newTab.GetComponent<Button>();
            tabButton.onClick.RemoveAllListeners();
            tabButton.onClick.AddListener((Action)(() =>
            {
                global::QuickMenu.prop_QuickMenu_0.field_Private_GameObject_6.SetActive(false);
                global::QuickMenu.prop_QuickMenu_0.field_Private_GameObject_6 = newMenu.gameObject;
                newMenu.gameObject.SetActive(true);
            }));
            
            newTab.transform.FindChild("Badge").gameObject.SetActive(false);

            // Allow invite menu to instantiate
            quickModeMenus.Find("QuickModeNotificationsMenu").gameObject.SetActive(true);
            quickModeMenus.Find("QuickModeNotificationsMenu").gameObject.SetActive(false);
            
            CopyConsoleScreen(newMenu);

            return (newTab, newMenu);
        }

        private static void SetTabIndex(Transform tab, MonoBehaviourPublicObCoGaCoObCoObCoUnique.EnumNPublicSealedvaHoNoPl4vUnique value)
        {
            MonoBehaviour tabDescriptor = tab.GetComponents<MonoBehaviour>().First(c => c.GetIl2CppType().GetMethod("ShowTabContent") != null);

            tabDescriptor.GetIl2CppType().GetFields().First(f => f.FieldType.IsEnum).SetValue(tabDescriptor, new Il2CppSystem.Int32 { m_value = (int)value }.BoxIl2CppObject());
        }


        private static Sprite CreateSpriteFromBase64(string data)
        {
            Texture2D t = new Texture2D(2, 2);
            ImageConversion.LoadImage(t, Convert.FromBase64String(data));
            Rect rect = new Rect(0.0f, 0.0f, t.width, t.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Vector4 border = Vector4.zero;

            Sprite s = Sprite.CreateSprite_Injected(t, ref rect, ref pivot, 100.0f, 0, SpriteMeshType.Tight, ref border, false);

            return s;
        }

        private static void CopyConsoleScreen(Transform newTab)
        {
            var originalConsole = GameObject.Find("UserInterface/QuickMenu/AvatarStatsMenu");
            var newConsole = Object.Instantiate(originalConsole, newTab);
            newConsole.name = "MLConsole";
            newConsole.transform.localPosition = new Vector3(0, -625, 0);
            Object.Destroy(newConsole.gameObject.GetComponent<VRCUiAvatarStatsPanel>());
            Object.Destroy(newConsole.transform.FindChild("Panel").gameObject);
            
            DecorateConsole(newConsole.transform);
            DecorateButtons(newConsole.transform);
            newConsole.SetActive(true);
        }

        private static void DecorateConsole(Transform consoleTransform)
        {
            var ratingText = consoleTransform.FindChild("_Console/_Header/Text_Rating");
            var icons = consoleTransform.FindChild("_Console/_Header/_ICONS");
            var overallText = consoleTransform.FindChild("_Console/_Header/Text_Overall");
            var icon = consoleTransform.FindChild("_Console/_Header/InfoIcon");
            
            Object.Destroy(ratingText.gameObject);
            Object.Destroy(icons.gameObject);

            overallText.GetComponent<Text>().text = "MelonLoader Console";
            icon.GetComponent<Image>().sprite = CreateSpriteFromBase64(melonIcon);
        }

        private static void DecorateButtons(Transform consoleTransform)
        {
            var backButton = consoleTransform.FindChild("_Buttons/_BackButton");
            var documentationButtonText = consoleTransform.FindChild("_Buttons/DocumentationButton/Text (1)");
            var documentationButton = consoleTransform.FindChild("_Buttons/DocumentationButton");
            
            Object.Destroy(backButton.gameObject);
            Object.Destroy(documentationButtonText.gameObject);

            documentationButton.localPosition = new Vector3(623, -1111, 0);
            documentationButton.FindChild("Text").GetComponent<Text>().text = "Clear Console";

            var console = consoleTransform.FindChild("_Console/_StatsConsole");
            console.GetComponent<ScrollRect>().horizontal = true;

            var viewport = consoleTransform.FindChild("_Console/_StatsConsole/Viewport");
            viewport.localPosition = Vector3.zeroVector;
            var viewportContent = viewport.FindChild("Content");
            viewportContent.localPosition = Vector3.zeroVector;
            viewportContent.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.MinSize;

            new TextLine(viewportContent);
            
            viewportContent.localPosition = Vector3.zeroVector;
            viewport.localPosition = Vector3.zeroVector;
        }
    }
}