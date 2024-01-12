using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CoinsManager : MonoBehaviour {
    public static CoinsManager Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI[] _coinsTexts;

    private static UnityEvent m_OnCoinsAmountChanged;

    private void OnEnable() {
        if (m_OnCoinsAmountChanged == null)
            m_OnCoinsAmountChanged = new UnityEvent();

        m_OnCoinsAmountChanged.AddListener(UpdateCoinsDisplay);
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        ChangeCoinsAmount(50);
    }

    private void Start() {
        UpdateCoinsDisplay();
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void UpdateCoinsDisplay() {
        if (_coinsTexts != null) {
            foreach (var textComponent in _coinsTexts) {
                if (textComponent != null) {
                    textComponent.text = PlayerPrefs.GetInt("Coins", 0).ToString();
                }
            }
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public static void ChangeCoinsAmount(int amount) {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + amount);
        m_OnCoinsAmountChanged?.Invoke();
    }

    private void OnDisable() {
        m_OnCoinsAmountChanged.RemoveListener(UpdateCoinsDisplay);
    }
}
