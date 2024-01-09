using UnityEngine;

public class SettingsScreenController : UIController {
    [SerializeField] private float _duration = 1f;

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnEnable() {
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Appear(_duration);
    }

    private void Start(){
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Appear(_duration);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void Close(){
        for (int i = 0; i < _tweenObjects.Count; i++)
            _tweenObjects[i].Disappear(_duration);
    }
}
