using UnityEngine;
using UnityEngine.UI;

public class SettingsScreenController : UIController {
    [SerializeField] private Button _soundButton, _vibrationButton, _joystickButton;
    [SerializeField] private Sprite _toggleOn, _toggleOff;

    protected override void Start() {
        base.Start();

        UpdateButtonState(_soundButton, PlayerPrefsManager.GetMusicEnabled());
        UpdateButtonState(_vibrationButton, PlayerPrefsManager.GetVibrationEnabled());
        UpdateButtonState(_joystickButton, PlayerPrefsManager.GetJoystickEnabled());
    }

    private void UpdateButtonState(Button button, bool isEnabled) {
        button.image.sprite = isEnabled ? _toggleOn : _toggleOff;
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void ToggleSound() {
        bool isMusicEnabled = !PlayerPrefsManager.GetMusicEnabled();
        PlayerPrefsManager.SetMusicEnabled(isMusicEnabled);
        UpdateButtonState(_soundButton, isMusicEnabled);
        AudioListener.volume = isMusicEnabled ? 1 : 0;
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void ToggleVibration() {
        bool isVibrationEnabled = !PlayerPrefsManager.GetVibrationEnabled();
        PlayerPrefsManager.SetVibrationEnabled(isVibrationEnabled);
        UpdateButtonState(_vibrationButton, isVibrationEnabled);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void ChangeJoystickType() {
        bool isJoystickEnabled = !PlayerPrefsManager.GetJoystickEnabled();
        PlayerPrefsManager.SetJoystickEnabled(isJoystickEnabled);
        UpdateButtonState(_joystickButton, isJoystickEnabled);
    }
}
