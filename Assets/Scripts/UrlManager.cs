using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UrlManager : MonoBehaviour
{
    [SerializeField] private string _urlForMoreGames;
    [SerializeField] private string _urlForPrivacyPolicy;
    [SerializeField] private string _urlForRateUs;
    
    [SerializeField] private Button _privacyButton; 
    [SerializeField] private Button _moreGamesButton;
    [SerializeField] private Button _rateUsButton;

    private bool _externalOpeningUrlDelayFlag = false;

    private void Awake()
    {
        if (_rateUsButton != null)
            _rateUsButton.onClick.AddListener(() => OpenUrl(_urlForRateUs));

        if (_privacyButton != null)
            _privacyButton.onClick.AddListener(() => OpenUrl(_urlForPrivacyPolicy));
        
        if (_moreGamesButton != null)
        {
            _moreGamesButton.onClick.AddListener(() => OpenUrl(_urlForMoreGames));
        }
    }

    private void OnDestroy()
    {
        if (_rateUsButton != null)
            _rateUsButton.onClick.RemoveListener(() => OpenUrl(_urlForRateUs));

        if (_privacyButton != null)
            _privacyButton.onClick.RemoveListener(() => OpenUrl(_urlForPrivacyPolicy));
        
        if (_moreGamesButton != null)
        {
            _moreGamesButton.onClick.RemoveListener(() => OpenUrl(_urlForMoreGames));
        }
    }

    private async void OpenUrl(string url)
    {
        if (_externalOpeningUrlDelayFlag) return;
        _externalOpeningUrlDelayFlag = true;
        await OpenURLAsync(url);
        StartCoroutine(WaitForSeconds(1, () => _externalOpeningUrlDelayFlag = false));
    }
    
    private async Task OpenURLAsync(string url)
    {
        await Task.Delay(1); // Ждем один кадр, чтобы избежать блокировки основного потока
        try
        {
            Application.OpenURL(url); // Открываем ссылку
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка при открытии ссылки {url}: {e.Message}");
        }
    }

    private IEnumerator WaitForSeconds(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    } 
}