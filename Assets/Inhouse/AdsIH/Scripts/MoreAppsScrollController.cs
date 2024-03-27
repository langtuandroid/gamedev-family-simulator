using Inhouse.AdsIH.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Area730.MoreAppsPage
{

    public class MoreAppsScrollController : MonoBehaviour
    {

        [SerializeField]
        private Image _itemPrefab;
        [SerializeField]
        private GameObject CrossBTN;
        [SerializeField]
        private ContentSizeFitter _fitter;

        private int _itemCount = 0;

        private void Awake()
        {
            CrossBTN.SetActive(false);
            _fitter.gameObject.SetActive(false);
        }
        public void AddItem(MoreAppsHandler.ItemContainer itemData)
        {
            Image item = Instantiate(_itemPrefab) as Image;
            ItemView view = item.GetComponent<ItemView>();

            view.btn.onClick.AddListener(itemData.btnAction);
            view.appName.text = itemData.appName;
            view.icon.sprite = itemData.sprite;

            item.transform.SetParent(_fitter.transform);

            item.transform.localScale = Vector3.one;

            ++(_itemCount);
            if(CrossBTN)
                CrossBTN.SetActive(true);
            _fitter.gameObject.SetActive(true);
        }

        public void ClearList()
        {
            _itemCount = 0;

            foreach (Transform item in _fitter.transform)
            {
                if (item != _fitter.transform)
                {
                    Destroy(item.gameObject);
                }
            }
        }

        public int ItemCount { get { return _itemCount; } }

    }

}