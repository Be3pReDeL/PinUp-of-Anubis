using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _fallThreshold = -0.5f, _jumpThreshold = 0.5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private Button[] _healthButtons;
    [SerializeField] private Sprite[] _skins; // Массив спрайтов скинов

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded = true;
    private int _currentHealth;
    private Button _spellButton;
    private TextMeshProUGUI _projectileCountText;

    private void OnEnable(){
        _spellButton = GameObject.FindGameObjectWithTag("Fire Button").GetComponent<Button>();
        _spellButton.onClick.AddListener(Fire);
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Выбор скина в зависимости от сохраненного значения
        int currentSkinIndex = PlayerPrefsManager.GetCurrentSkin();
        if (currentSkinIndex >= 0 && currentSkinIndex < _skins.Length) {
            _spriteRenderer.sprite = _skins[currentSkinIndex];
        }

        _currentHealth = _maxHealth;

        _projectileCountText = GameObject.FindGameObjectWithTag("SpellCountText").GetComponent<TextMeshProUGUI>();

        _healthButtons = new Button[3];

        for(int i = 0; i < 3; i++){
            _healthButtons[i] = GameObject.FindGameObjectWithTag(string.Format("Health{0}", i + 1)).GetComponent<Button>();
        }

        UpdateProjectileCountUI();
        UpdateHealthUI();
    }

    private void Update() {
        float moveInput = SimpleInput.GetAxis("Horizontal");
        _rb.velocity = new Vector2(moveInput * _moveSpeed, _rb.velocity.y);

        if (moveInput != 0) {
            _spriteRenderer.flipX = moveInput < 0;
        }

        if (SimpleInput.GetAxis("Vertical") > _jumpThreshold && _isGrounded) {
            _rb.velocity = Vector2.up * _jumpForce;
            _isGrounded = false;
            StartCoroutine(DisableCollisionTemporarily());
        }

        if (SimpleInput.GetAxis("Vertical") < _fallThreshold) {
            StartCoroutine(DisableCollisionTemporarily());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            _isGrounded = true;
        }
    }

    private IEnumerator DisableCollisionTemporarily() {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Ground"), true);
        yield return new WaitForSeconds(1.5f);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Ground"), false);
    }

    public void Fire() {
        if (PlayerPrefsManager.GetScrolls() > 0) {
            FireProjectile();

            UpdateProjectileCountUI();
        }
    }

    private void FireProjectile() {
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
        float projectileSpeed = 30f; // Увеличиваем скорость Projectile'ов
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2((_spriteRenderer.flipX ? -1 : 1) * projectileSpeed, 0);

        PlayerPrefsManager.SetScrolls(PlayerPrefsManager.GetScrolls() - 1);

        Destroy(projectile, 2f); // Уничтожаем Projectile через 2 секунды
    }


    private void UpdateProjectileCountUI() {
        _projectileCountText.text = PlayerPrefsManager.GetScrolls().ToString();
    }

    public void TakeDamage() {
        if (_currentHealth > 0) {
            _currentHealth--;
            UpdateHealthUI();
            if (_currentHealth <= 0) {
                GameOver();
            }
        }
    }

    private void UpdateHealthUI() {
        for (int i = 0; i < _healthButtons.Length; i++) {
            _healthButtons[i].interactable = i < _currentHealth;
        }
    }

    public void AddLife() {
        if (_currentHealth < _maxHealth) {
            _currentHealth++;
            UpdateHealthUI();
        }
    }

    private void GameOver() {
        GameManager.Instance.Death();

        _spriteRenderer.enabled = false;
        enabled = false;
    }

    private void OnDisable(){
        _spellButton.onClick.RemoveListener(Fire);
    }
}
