using UnityEngine;

public class SixtyFPS : MonoBehaviour {
    [SerializeField] private int targetFrameRate = 60;

    private void Awake() {
        Application.targetFrameRate = targetFrameRate;
    }
}
