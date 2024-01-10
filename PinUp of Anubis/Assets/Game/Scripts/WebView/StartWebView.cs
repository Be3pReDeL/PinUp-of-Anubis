using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UniWebViewNamespace;

public class StartWebView : MonoBehaviour {
    [SerializeField] private RectTransform _webViewRectTransform;
    private string _adsID;

    private void Awake() {
        if (PlayerPrefs.GetInt("Got Ads ID?", 0) != 0) {
            Application.RequestAdvertisingIdentifierAsync((string advertisingId, bool trackingEnabled, string error) => {
                _adsID = advertisingId;
            });
        }
    }

    private void Start() {
        if (Application.internetReachability != NetworkReachability.NotReachable) {
            string url = PlayerPrefs.GetString("URL", string.Empty);
            if (!string.IsNullOrEmpty(url))
                StartCoroutine(LoadWebViewWithDelay(1.5f, url));
            else
                StartCoroutine(ProcessOfferLink(ChooseWhichToLoad.URLToShow));
        } else {
            LoadScene.LoadNextScene();
        }
    }

    private void ShowWebView(string url, string naming = "") {
        UniWebView.SetAllowInlinePlay(true);

        UniWebView webView = gameObject.AddComponent<UniWebView>();
        webView.ReferenceRectTransform = _webViewRectTransform;
        webView.SetToolbarDoneButtonText("");

        ConfigureToolbar(webView, naming);
        ConfigureWebViewEvents(webView);

        webView.Load(url);
        webView.Show();
    }

    private void ConfigureToolbar(UniWebView webView, string naming) {
        switch (naming) {
            case "0":
                webView.SetShowToolbar(true, false, false, true);
                break;
            default:
                webView.SetShowToolbar(false);
                break;
        }
    }

    private void ConfigureWebViewEvents(UniWebView webView) {
        webView.OnShouldClose += (view) => false;

        webView.SetSupportMultipleWindows(true, true);
        webView.OnMultipleWindowOpened += (view, windowId) => webView.SetShowToolbar(true);
        webView.OnMultipleWindowClosed += (view, windowId) => ConfigureToolbar(view, "0");

        webView.SetAllowBackForwardNavigationGestures(true);
        webView.OnPageFinished += (view, statusCode, url) => {
            if (PlayerPrefs.GetString("URL", string.Empty) == string.Empty)
                PlayerPrefs.SetString("URL", url);
        };
    }

    private IEnumerator LoadWebViewWithDelay(float delay, string link) {
        yield return new WaitForSeconds(delay);
        ShowWebView(link);
    }

    private IEnumerator ProcessOfferLink(string url) {
        using (UnityWebRequest www = UnityWebRequest.Get(url)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                LoadScene.LoadNextScene();
                yield break;
            }

            int delay = 3;
            while (PlayerPrefs.GetString("glrobo", "") == "" && delay > 0) {
                yield return new WaitForSeconds(1);
                delay--;
            }

            try {
                string fullUrl = ChooseWhichToLoad.URLToShow + "?idfa=" + _adsID + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", "");
                ShowWebView(fullUrl);
            } catch {
                LoadScene.LoadNextScene();
            }
        }
    }
}
