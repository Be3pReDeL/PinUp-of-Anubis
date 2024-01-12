using UnityEngine;

public class SetControllerType : MonoBehaviour
{
    public static SetControllerType Instance { get; private set; }

    [SerializeField] private GameObject _joystick, _dpad;

    private void Awake(){
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start(){
        UpdateJoystickType();
    }

    public void UpdateJoystickType(){
        _joystick.SetActive(PlayerPrefsManager.GetJoystickEnabled());
        _dpad.SetActive(!PlayerPrefsManager.GetJoystickEnabled());
    }
}
