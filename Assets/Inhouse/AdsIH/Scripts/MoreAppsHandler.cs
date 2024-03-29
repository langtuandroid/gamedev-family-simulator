// using System.Collections;
// using System.Collections.Generic;
// using System.IO;
// using SimpleJSON;
// using UnityEngine;
//
// namespace Inhouse.AdsIH.Scripts
// {
//     [RequireComponent(typeof(MoreAppsScrollController))]
//     public class MoreAppsHandler : MonoBehaviour
//     {
//         public static MoreAppsHandler instance;
//         
//         [SerializeField] private AudioClip btnClick;
//         private string _link;
//         public bool timeCheck = false;
//
//         private bool _isAlreadyUpdated;
//
//         #region Declarations and constants
//
//         private const string KeyVersion = "Version";
//         private const string KeyAppList = "AppsList";
//         private const string KeyIOS = "-iOS";
//         private const string KeyAndroid = "-Android";
//
//
//         private void Awake()
//         {
//             instance = this;
//         }
//
//         public class ItemContainer
//         {
//             public Sprite sprite;
//             public string appName;
//             public UnityEngine.Events.UnityAction btnAction;
//         }
//
//         private class AppItem
//         {
//             public string AppTitle { get; set; }
//             public string IconUrl { get; set; }
//             public string Id { get; set; }
//
//             public AppItem()
//             {
//             }
//
//             public void Print()
//             {
//                 Debug.Log("Title: " + AppTitle + ", icon url: " + IconUrl + ", id: " + Id);
//             }
//         }
//
//         private class MoreAppsDescriptor
//         {
//             public int AppsCount { get; set; }
//             public int Version { get; set; }
//             public List<AppItem> Items { get; set; }
//
//             public MoreAppsDescriptor()
//             {
//                 Items = new List<AppItem>();
//             }
//
//             public void Print()
//             {
//                 Debug.Log("Version: " + Version + ", AppsCount: " + AppsCount);
//             }
//         }
//
//         private class CoroutineWithData
//         {
//             public Coroutine coroutine { get; private set; }
//             public object result;
//             private IEnumerator target;
//
//             public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
//             {
//                 this.target = target;
//                 this.coroutine = owner.StartCoroutine(Run());
//             }
//
//             private IEnumerator Run()
//             {
//                 while (target.MoveNext())
//                 {
//                     result = target.Current;
//                     yield return result;
//                 }
//             }
//         }
//
//         #endregion
//
//         #region Vars
//
//         private readonly bool _logOn = false;
//
//
//         //private Button              _moreAppsBtn;
//
//         public string configFileUrl;
//
//         private MoreAppsDescriptor _currentDesc = null;
//         private static bool _updatesChecked = false;
//
//         #endregion
//
//         #region Utils
//
//         private static AppItem parseItem(JSONNode node)
//         {
//             AppItem result = new AppItem();
//
//             result.AppTitle = node["AppTitle"];
//             result.IconUrl = node["IconUrl"];
//             result.Id = node["Id"];
//
//             return result;
//         }
//
//         private static MoreAppsDescriptor ParseResponse(string json)
//         {
// #if UNITY_IOS
//         string platformPref = KEY_IOS;
// #else
//             string platformPref = KeyAndroid;
// #endif
//
//             try
//             {
//                 MoreAppsDescriptor result = new MoreAppsDescriptor();
//                 JSONNode rootNode = JSON.Parse(json);
//                 result.Version = rootNode[KeyVersion].AsInt;
//                 JSONArray appsArray = rootNode[KeyAppList + platformPref].AsArray;
//                 result.AppsCount = appsArray.Count;
//
//                 for (int i = 0; i < appsArray.Count; ++i)
//                 {
//                     AppItem item = parseItem(appsArray[i]);
//                     result.Items.Add(item);
//                 }
//
//                 return result;
//             }
//             catch (System.Exception e)
//             {
//                 Debug.LogError("MoreAppsPage: Json parse error");
//             }
//
//             return null;
//         }
//
//         private IEnumerator DownloadJsonDescriptor()
//         {
//             WWW www = new WWW(configFileUrl);
//             yield return www;
//
//             if (www.error != null)
//             {
//                 Debug.LogError("MoreAppsPage: WWW error: " + www.error);
//             }
//             else
//             {
//                 if (_logOn)
//                 {
//                     Debug.Log("MoreAppsPage: Descriptor downloaded");
//                 }
//
//                 yield return StartCoroutine(UpdateMoreAppsPage(www.text));
//             }
//         }
//
//         private UnityEngine.Events.UnityAction getBtnAction(string id)
//         {
//             return () =>
//             {
// #if UNITY_ANDROID
// 			if (PlayerPrefs.GetInt ("AmazonAds") == 1) {
// 			_link = "amzn://apps/android?p=" + id;
// 			}
// 			else{
// 			_link = "https://play.google.com/store/apps/details?id=" + id;
// 			}
// #elif UNITY_IOS
//             link = "itms-apps://itunes.apple.com/app/id" + id;
// #endif
//
//
//                 Application.OpenURL(_link);
//
// //			GoogleAnalyticsV3.instance.LogScreen (id);
//                 if (timeCheck == false && Time.timeScale < 1.0f)
//                 {
//                     Time.timeScale = 1;
//                     timeCheck = true;
//                 }
//
//                 BundleIdList.instance.onlyOnDisplay.Remove(id);
//                 //StartCoroutine(bbbb(link));
//                 StartCoroutine(UpdateUI());
//                 if (timeCheck == true)
//                 {
//                     Time.timeScale = 0.01f;
//                     timeCheck = false;
//                 }
//             };
//         }
//
// //	IEnumerator bbbb (string linktogame) {
// //		yield return new WaitForSeconds (0.3f);
// //
// //		StopCoroutine (bbbb (linktogame));
// //
// //	}
//
//         private void CheckForUpdates()
//         {
//             _updatesChecked = true;
//             StartCoroutine(DownloadJsonDescriptor());
//         }
//
//         private void LoadConfig()
//         {
//             string oldFile = File.ReadAllText(Utilswl.GetSettingsFilePath());
//             _currentDesc = ParseResponse(oldFile);
//         }
//
//         #endregion
//
//         private IEnumerator UpdateMoreAppsPage(string responseJson)
//         {
//             MoreAppsDescriptor desc = ParseResponse(responseJson);
//
//             if (desc == null)
//             {
//                 Debug.LogError("MoreAppsPage: Error parsing downloaded descriptor file");
//                 yield break;
//             }
//
//             if (Utilswl.ConfigFileExists())
//             {
//                 if (desc.Version > 0)
//                 {
//                     // remove old images that wont be replaced
//                     for (int i = 0; i < desc.AppsCount; ++i)
//                     {
//                         string imagePath = Utilswl.GetImagePath(i);
//                         if (File.Exists(imagePath))
//                         {
//                             File.Delete(imagePath);
//                         }
//                     }
//
//                     File.WriteAllText(Utilswl.GetSettingsFilePath(), responseJson);
//                     _currentDesc = desc;
//
//                     yield return StartCoroutine(UpdateUI(forceReplace: true));
//                 }
//                 else
//                 {
//                     if (_logOn)
//                     {
//                         Debug.Log("MoreAppsPage: Downloaded version is not new.");
//                     }
//                 }
//             }
//             else
//             {
//                 if (_logOn)
//                 {
//                     Debug.Log("MoreAppsPage: First time file load");
//                 }
//
//                 File.WriteAllText(Utilswl.GetSettingsFilePath(), responseJson);
//                 _currentDesc = desc;
//
//                 yield return StartCoroutine(UpdateUI());
//                 _isAlreadyUpdated = true;
//             }
//         }
//
//         public IEnumerator UpdateUI(bool forceReplace = false)
//         {
//             if (_currentDesc == null)
//             {
//                 Debug.LogError("MoreAppsPage: desciptor is NULL");
//                 yield break;
//             }
//
//             MoreAppsScrollController scrollViewController = GetComponent<MoreAppsScrollController>();
//
//
// //        for (int i = 0; i < _currentDesc.AppsCount; ++i)
// //        {
// //            if (!File.Exists(Utils.GetImagePath(i)) || forceReplace)
// //            {
// //                CoroutineWithData cd = new CoroutineWithData(this, downloadImage(i));
// //                yield return cd.coroutine;
// //
// //                bool success = (bool)cd.result;
// //                if (!success)
// //                {
// //                    Debug.LogError("MoreAppsPage: Failded to load image");
// //                    continue;
// //                }
// //            }
// //
// //            if (!listEmpty)
// //            {
// //                // one time lazy clear
// //                scrollViewController.ClearList();
// //                listEmpty = true;
// //            }
// //
// //            ItemContainer itemData  = new ItemContainer();
// //            itemData.appName        = _currentDesc.Items[i].AppTitle;
// //            string id               = _currentDesc.Items[i].Id;
// //            itemData.btnAction      = getBtnAction(id);
// //            itemData.sprite         = Utils.GetSprite(i);
// //
// //            scrollViewController.AddItem(itemData);
// //
// //        }
//
//
//             if (_currentDesc.AppsCount > 0)
//             {
//                 int i = 0;
//                 //lock (BundleIdList.instance.locker) 
//
//                 i = UnityEngine.Random.Range(0, _currentDesc.AppsCount);
//                 print("count " + _currentDesc.AppsCount);
//                 print("up =>" + i + " " + _currentDesc.Items[i].Id);
// #if UNITY_ANDROID
// 			for (int j =
//  0; j <= _currentDesc.AppsCount && (BundleIdList.instance.urls.Contains (_currentDesc.Items [i].Id) || isAppInstalled(_currentDesc.Items [i].Id));) {
// 				j++;
// 				i = (i + j) % _currentDesc.AppsCount; 	
// 				print ("b1 =>" + i + " " + _currentDesc.Items [i].Id);
// 			}
//
//
// 			for (int j =
//  0; j <= _currentDesc.AppsCount && (BundleIdList.instance.onlyOnDisplay.Contains (_currentDesc.Items [i].Id) || isAppInstalled(_currentDesc.Items [i].Id)); ) {
// 				j++;
// 				i = (i + j) % _currentDesc.AppsCount; 		
// 				print ("b2 =>" +i);
// 			}
// #endif
//                 BundleIdList.instance.urls.Add(_currentDesc.Items[i].Id);
//                 BundleIdList.instance.onlyOnDisplay.Add(_currentDesc.Items[i].Id);
//                 print("Finalized " + i + " " + _currentDesc.Items[i].Id);
//
//                 //if (!File.Exists (Utilswl.GetImagePath (i)) || true)
//                 {
//                     CoroutineWithData cd = new CoroutineWithData(this, DownloadImage(i));
//                     yield return cd.coroutine;
//
//                     bool success = (bool)cd.result;
//                     if (!success)
//                     {
//                         Debug.LogError("MoreAppsPage: Failded to load image");
//                         //continue;
//                     }
//                 }
//
//                 scrollViewController.ClearList();
//                 ItemContainer itemData = new ItemContainer();
//                 itemData.appName = _currentDesc.Items[i].AppTitle;
//                 itemData.btnAction = getBtnAction(_currentDesc.Items[i].Id);
//                 itemData.sprite = Utilswl.GetSprite(i);
//                 scrollViewController.AddItem(itemData);
// //			if (BigImageController.mine == 1){
// //				GameObject.Find ("inhouse").transform.GetChild(2).gameObject.SetActive(true);
// //				BigImageController.mine = 2;
// //			}
//                 if (GameObject.Find("Loading") != null)
//                 {
//                     GameObject.Find("Loading").SetActive(false);
//                 }
//
// //			GoogleAnalyticsV3.instance.LogScreen (itemData.appName.ToString());
//             }
//             else
//                 scrollViewController.ClearList();
//
//             if (!forceReplace && !_updatesChecked)
//             {
//                 CheckForUpdates();
//             }
//         }
//
// #if UNITY_ANDROID
// 	private bool isAppInstalled(string bundleID) 
// 	{
// 			#if UNITY_EDITOR
// 				return false;
// 			#endif
// 			#if UNITY_ANDROID
// #endif
// 		}
// #endif
//
//         private void LoadStaticItems()
//         {
//             ItemLoader loader = GetComponent<ItemLoader>();
//             if (loader != null)
//             {
//                 MoreAppsScrollController scrollViewController = GetComponent<MoreAppsScrollController>();
// #if UNITY_IOS
//             ItemLoader.ItemElement[] itemArray = loader.IosApps;
// #else
//                 ItemLoader.ItemElement[] itemArray = loader.androidApps;
// #endif
//
//                 foreach (ItemLoader.ItemElement e in itemArray)
//                 {
//                     ItemContainer itemData = new ItemContainer();
//                     itemData.sprite = e.appIcon;
//                     itemData.appName = e.appName;
//                     itemData.btnAction = getBtnAction(e.appId);
//
//                     scrollViewController.AddItem(itemData);
//                 }
//
//                 if (scrollViewController.ItemCount > 0)
//                 {
//                     //_moreAppsBtn.gameObject.SetActive(true);
//                 }
//
//                 Destroy(loader, 0.1f);
//             }
//         }
//
//         private IEnumerator DownloadImage(int index)
//         {
//             WWW www = new WWW(_currentDesc.Items[index].IconUrl);
//
//             yield return www;
//
//
//             if (www.error != null)
//             {
//                 Debug.LogError("MoreAppsPage: WWW error: " + www.error);
//                 yield return false;
//             }
//             else
//             {
//                 File.WriteAllBytes(Utilswl.GetImagePath(index), www.bytes);
//                 yield return true;
//             }
//         }
//
//         #region Unity methods
//
//         void Start()
//         {
//             //_moreAppsBtn.gameObject.SetActive(false);
//
//             //        loadStaticItems();
//             //
//             //		if (Utilswl.ConfigFileExists())
//             //        {
//             //            loadConfig();
//             //            StartCoroutine(updateUI());
//             //        } else
//             //        {
//             //        }
//
//             if (SystemInfo.systemMemorySize > 2500)
//             {
//                 CheckForUpdates();
//             }
//
//
//             if (_logOn)
//             {
//                 Debug.Log("MoreAppsPage: data path: " + Utilswl.GetSettingsFilePath());
//             }
//         }
//
//         #endregion
//     }
// }