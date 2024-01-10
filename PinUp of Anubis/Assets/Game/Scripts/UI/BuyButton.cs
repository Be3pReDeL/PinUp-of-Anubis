using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuyButton : MonoBehaviour {
    public enum PurchaseType {
        Skin,
        Scroll
    }

    [SerializeField] private int _cost = 50;
    [SerializeField] private int _skinIndex = 0;
    [SerializeField] private PurchaseType _purchaseType = PurchaseType.Skin;

    private GameObject _coinImage;
    private TextMeshProUGUI _costStatusText;
    private Button _thisButton;

    // Статическое событие для обновления всех кнопок
    public static event Action OnPurchaseMade = delegate { };

    private void Awake() {
        transform.GetChild(0).TryGetComponent(out _costStatusText);
        _coinImage = _costStatusText.transform.GetChild(0).gameObject;
        _thisButton = GetComponent<Button>();

        OnPurchaseMade += UpdateButtonState; // Подписываемся на событие
    }

    private void OnDestroy() {
        OnPurchaseMade -= UpdateButtonState; // Отписываемся от события
    }

    private void Start() {
        UpdateButtonState();
    }

    private void UpdateButtonState() {
        bool hasEnoughCoins = PlayerPrefsManager.GetCoins() >= _cost;
        bool isSkinOwned = PlayerPrefsManager.IsSkinOpened(_skinIndex);
        bool isSkinSelected = PlayerPrefsManager.GetCurrentSkin() == _skinIndex;

        if (_purchaseType == PurchaseType.Skin) {
            _thisButton.interactable = !isSkinSelected && (isSkinOwned || hasEnoughCoins);
            _costStatusText.text = isSkinSelected ? "Selected" : isSkinOwned ? "Select" : _cost.ToString();
            _coinImage.SetActive(!isSkinOwned);
        } else if (_purchaseType == PurchaseType.Scroll) {
            _thisButton.interactable = hasEnoughCoins;
            _costStatusText.text = _cost.ToString();
            _coinImage.SetActive(true);
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void Buy() {
        switch (_purchaseType) {
            case PurchaseType.Skin:
                BuySkin();
                break;
            case PurchaseType.Scroll:
                BuyScroll();
                break;
        }

        CoinsManager.Instance.UpdateCoinsDisplay();
    }

    private void BuySkin() {
        PlayerPrefsManager.SetCoins(PlayerPrefsManager.GetCoins() - _cost);
        PlayerPrefsManager.OpenSkin(_skinIndex);
        PlayerPrefsManager.SetCurrentSkin(_skinIndex);
        OnPurchaseMade.Invoke(); // Вызов события после покупки
    }

    private void BuyScroll() {
        PlayerPrefsManager.SetScrolls(PlayerPrefsManager.GetScrolls() + 5);
        PlayerPrefsManager.SetCoins(PlayerPrefsManager.GetCoins() - _cost);
        OnPurchaseMade.Invoke(); // Вызов события после покупки
    }
}
