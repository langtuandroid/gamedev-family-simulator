using UnityEngine;
using UnityEngine.Serialization;

namespace Inhouse.AdsIH.Scripts
{
    [AddComponentMenu("More Apps/Item Loader")]
    public class ItemLoader : MonoBehaviour
    {
        [System.Serializable]
        public class ItemElement
        {
            [FormerlySerializedAs("AppIcon")] public Sprite appIcon;
            [FormerlySerializedAs("AppName")] public string appName;
            [FormerlySerializedAs("AppId")] public string appId;
        }

        [FormerlySerializedAs("AndroidApps")] public ItemElement[] androidApps;
        [FormerlySerializedAs("IosApps")] public ItemElement[] iosApps;


    }

}

