using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MelonViewer
{
    public class TabBadge
    {
        private GameObject badgeObject;
        private RawImage tabImage;
        private Text tabText;

        private Dictionary<MelonLogType, int> pendingLogNotifs = new Dictionary<MelonLogType, int>();
        
        public TabBadge(GameObject badgeObject)
        {
            this.badgeObject = badgeObject;
            tabImage = badgeObject.GetComponent<RawImage>();
            tabText = badgeObject.transform.Find("NotificationsText").GetComponent<Text>();
        }

        public void NotifyNewLog(MelonLog log)
        {
            if (pendingLogNotifs.ContainsKey(log.logType))
                pendingLogNotifs[log.logType] += 1;
            else
                pendingLogNotifs.Add(log.logType, 1);

            if (pendingLogNotifs.ContainsKey(MelonLogType.Error))
            {
                tabImage.color = Color.red;
                tabText.text = pendingLogNotifs[MelonLogType.Error].ToString();
                badgeObject.SetActive(true);
            }
        }

        public void OnTabViewed()
        {
            badgeObject.SetActive(false);
            pendingLogNotifs.Clear();
        }
    }
}