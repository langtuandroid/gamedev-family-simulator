using UnityEngine;

namespace Area730.MoreAppsPage
{
    [AddComponentMenu("More Apps/Item Loader")]
    public class ItemLoader : MonoBehaviour
    {

        [System.Serializable]
        public class ItemElement
        {
            public Sprite AppIcon;
            public string AppName;
            public string AppId;
        }

        public ItemElement[] AndroidApps;
        public ItemElement[] IosApps;


    }

}

