using UnityEngine;

public static class PlayerPrefsManager {
    private const string MUSICKEY = "Music";
    private const string VIBRATIONKEY = "Vibration";
    private const string JOYSTICKKEY = "Joystick";
    private const string COINSKEY = "Coins";
    private const string SKINOPENEDKEY = "Skin {0} Opened";
    private const string CURRENTSKINKEY = "Current Skin";
    private const string SCROLLSKEY = "Scrolls";

    public static bool GetMusicEnabled() {
        return PlayerPrefs.GetInt(MUSICKEY, 0) == 1;
    }

    public static void SetMusicEnabled(bool isEnabled){
        PlayerPrefs.SetInt(MUSICKEY, isEnabled ? 1 : 0);
    }

    public static bool GetVibrationEnabled(){
        return PlayerPrefs.GetInt(VIBRATIONKEY, 1) == 1;
    }

    public static void SetVibrationEnabled(bool isEnabled){
        PlayerPrefs.SetInt(VIBRATIONKEY, isEnabled ? 1 : 0);
    }

    public static bool GetJoystickEnabled(){
        return PlayerPrefs.GetInt(JOYSTICKKEY, 1) == 1;
    }

    public static void SetJoystickEnabled(bool isEnabled){
        PlayerPrefs.SetInt(JOYSTICKKEY, isEnabled ? 1 : 0);
    }

    public static int GetCoins(){
        return PlayerPrefs.GetInt(COINSKEY, 0);
    }

    public static void SetCoins(int amount){
        PlayerPrefs.SetInt(COINSKEY, amount);
    }

    public static bool IsSkinOpened(int skinIndex){
        return PlayerPrefs.GetInt(string.Format(SKINOPENEDKEY, skinIndex), 0) == 1;
    }

    public static void OpenSkin(int skinIndex){
        PlayerPrefs.SetInt(string.Format(SKINOPENEDKEY, skinIndex), 1);
    }

    public static int GetCurrentSkin(){
        return PlayerPrefs.GetInt(CURRENTSKINKEY, 0);
    }

    public static void SetCurrentSkin(int skinIndex){
        PlayerPrefs.SetInt(CURRENTSKINKEY, skinIndex);
    }

    public static int GetScrolls() {
        return PlayerPrefs.GetInt(SCROLLSKEY, 0);
    }

    public static void SetScrolls(int value) {
        PlayerPrefs.SetInt(SCROLLSKEY, value);
    }
}
