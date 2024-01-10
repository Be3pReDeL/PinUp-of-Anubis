using UnityEngine;

public class DontDestroy : MonoBehaviour {
    private static DontDestroy Instance { get; set;}

    private void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else {
            Destroy(this);
        }
    }
}
