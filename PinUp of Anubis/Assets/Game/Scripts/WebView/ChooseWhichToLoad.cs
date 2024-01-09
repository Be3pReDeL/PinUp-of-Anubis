using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Unity.Advertisement.IosSupport;
using Unity.Notifications;
using Mono.Cecil.Cil;

public class ChooseWhichToLoad : MonoBehaviour {
    public static int SceneIndex {get; private set;} = 2;
    public static string URLToShow {get; private set;}

    [SerializeField] private string _URL = "https://dev-rcovvc6yf0j9213.api.raw-labs.com/applinks?key=game";

    private ATTrackingStatusBinding.AuthorizationTrackingStatus _status;

    private void Start() {
#if UNITY_IOS
        ATTrackingStatusBinding.RequestAuthorizationTracking();

        var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        if(status == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
            PlayerPrefs.SetInt("Got Ads ID?", 1);
#endif

        StartCoroutine(RequestNotificationPermission());

        StartCoroutine(GetAPIAnswer());
    }

    private IEnumerator GetAPIAnswer() {
        UnityWebRequest www = UnityWebRequest.Get(_URL);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);

        else {
            string pageText = www.downloadHandler.text.Replace("\"", "");

            if(pageText == "stop"){
                SceneIndex = 2;

                Destroy(this);
            }

            else{
                Debug.Log(pageText);
                SceneIndex = 1;

                URLToShow = pageText;
            }
        }
    }

    private IEnumerator RequestNotificationPermission() {
#if UNITY_IOS
        var request = NotificationCenter.RequestPermission();

        if (request.Status == NotificationsPermissionStatus.RequestPending)
            yield return request;
#endif
    }
}
