using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SuperGameController : MonoBehaviour {
    [SerializeField] private Button[] gameButtons;
    [SerializeField] private Sprite[] prizeSprites; // Спрайты для выигрышей
    [SerializeField] private Sprite _origianlSprite;

    private void Start() {
        foreach (var button in gameButtons) {
            button.onClick.AddListener(() => ButtonClicked(button));
            button.image.sprite = _origianlSprite;
            button.interactable = true;
        }
    }

    private void ButtonClicked(Button clickedButton) {
        // Отключаем все кнопки
        foreach (var button in gameButtons) {
            button.interactable = false;
        }

        // Определяем случайный выигрыш
        int prizeIndex = Random.Range(0, 4);
        clickedButton.image.sprite = prizeSprites[prizeIndex]; // Меняем спрайт кнопки на спрайт выигрыша

        // Обработка выигрыша
        switch (prizeIndex) {
            case 0:
                AddExtraLife();
                break;
            case 1:
                AddCoins(10);
                break;
            case 2:
                Invoke(nameof(LoadNextLevel), 3);
                break;
            case 3:
                LoadAnubisBattle();
                break;
        }

        MakeTimeScaleOriginal();

        StartCoroutine(DisableAfterTime());
    }

    private void AddExtraLife() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().AddLife();
    }

    private void AddCoins(int amount) {
        // Добавляем монеты игроку
        CoinsManager.ChangeCoinsAmount(amount);
    }

    private void LoadNextLevel() {
        LoadScene.LoadSceneByRelativeIndex(0);
    }

    private void LoadAnubisBattle() {
        LoadScene.LoadSceneByRelativeIndex(1); // Предполагается, что сцена с битвой с Анубисом имеет индекс 1
    }

    private IEnumerator DisableAfterTime(){
        yield return new WaitForSeconds(2f);

        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void MakeTimeScaleOriginal(){
        Time.timeScale = 1f;
    }
}
