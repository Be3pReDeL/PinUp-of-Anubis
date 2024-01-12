using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _youLooseScreen, _youWinScreen;
    [SerializeField] private TextMeshProUGUI _looseCoinsCountText, _winCoinsCountText, _gameTime;

    private int coinsCollectedThisGame = 0;
    private int totalEnemies;
    private int enemiesDefeated;

    private float levelTimer;
    private bool levelInProgress;

    private bool win = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount) {
        coinsCollectedThisGame += amount;
        // Можно добавить здесь дополнительный код для обновления UI или других механик
    }

    public int GetCoinsCollectedThisGame() {
        return coinsCollectedThisGame;
    }

    public void RegisterEnemy() {
        totalEnemies++;
    }

    public void EnemyDefeated() {
        enemiesDefeated++;
        CheckForWin();
    }

     private void Start() {
        StartLevel();
    }

    private void Update() {
        if (levelInProgress) {
            levelTimer += Time.deltaTime;
        }
    }

    public void StartLevel() {
        levelTimer = 0f;
        levelInProgress = true;
    }

    public string EndLevel() {
        levelInProgress = false;
        
        int minutes = (int)levelTimer / 60;
        int seconds = (int)levelTimer % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float GetLevelTime() {
        return levelTimer;
    }

    private void CheckForWin() {
        if (enemiesDefeated >= totalEnemies) {
            win = true;
            _youWinScreen.SetActive(true);
            _winCoinsCountText.text = "+" + coinsCollectedThisGame.ToString();
            _gameTime.text = EndLevel();
        }
    }

    public void Death(){
        if(!win){
            _youLooseScreen.SetActive(true);
            _looseCoinsCountText.text = "+" + coinsCollectedThisGame.ToString();
        }   
    }
}
