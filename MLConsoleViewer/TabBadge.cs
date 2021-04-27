using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MelonViewer
{
    public class TabBadge
    {
        private readonly GameObject _badgeObject;
        private readonly RawImage _tabImage;
        private readonly Text _tabText;

        private readonly Dictionary<MelonLogType, int> _pendingLogNotifs = new Dictionary<MelonLogType, int>();
        
        public TabBadge(GameObject badgeObject)
        {
            _badgeObject = badgeObject;
            _tabImage = badgeObject.GetComponent<RawImage>();
            _tabText = badgeObject.transform.Find("NotificationsText").GetComponent<Text>();
        }

        public void NotifyNewLog(MelonLog log)
        {
            if (_pendingLogNotifs.ContainsKey(log.LogType))
                _pendingLogNotifs[log.LogType] += 1;
            else
                _pendingLogNotifs.Add(log.LogType, 1);

            if (!_pendingLogNotifs.ContainsKey(MelonLogType.Error)) return;
            
            _tabImage.color = Color.red;
            _tabText.text = _pendingLogNotifs[MelonLogType.Error].ToString();
            _badgeObject.SetActive(true);
        }

        public void OnTabViewed()
        {
            _badgeObject.SetActive(false);
            _pendingLogNotifs.Clear();
        }
    }
}