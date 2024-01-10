using UnityEngine;

[OPS.Obfuscator.Attribute.DoNotObfuscateClass]
public class SetRotation : MonoBehaviour {
    [SerializeField] private ScreenOrientation _screenOrientation;
    private ScreenOrientation _originalOrientation;

    private void Start() {
        _originalOrientation = Screen.orientation;
        Screen.orientation = _screenOrientation;
    }

    private void OnDestroy() {
        // Возвращаем ориентацию экрана к исходной при уничтожении объекта
        Screen.orientation = _originalOrientation;
    }
}
