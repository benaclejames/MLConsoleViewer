using System;
using System.Collections.Generic;
using System.Linq;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MelonViewer.QuickMenu
{
    public static class QuickModeMenu
    {
        public static bool HasInitMenu;

        private static GameObject OriginalTabsObject =>
            GameObject.Find("UserInterface/QuickMenu/QuickModeMenus/QuickModeNotificationsMenu/NotificationTabs");


        public static void InitializeMenu()
        {
            CreateNotificationTab("MLConsoleViewer", "Text", Color.magenta, // Organization 100
                "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");
            
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
            
            HandleMenuTabCreation(newMenu);

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

        private static void HandleMenuTabCreation(Transform newMenu)
        {
            var newTabs =
                Object.Instantiate(OriginalTabsObject, OriginalTabsObject.transform.parent, true);

            newTabs.name = "MLConsoleViewer";

            newTabs.transform.localScale = OriginalTabsObject.transform.localScale;
            newTabs.transform.localPosition = OriginalTabsObject.transform.localPosition;
            newTabs.transform.localRotation = OriginalTabsObject.transform.localRotation;

            //Strip away notification menu scripts and instantiate top buttons
            for (var i=0; i < newTabs.transform.GetChildCount(); i++)
            {
                var tab = newTabs.transform.GetChild(i);
                Object.Destroy(tab.gameObject);
            }

            newTabs.transform.parent = newMenu;
        }
    }
}