using UnityEngine;
using UnityEngine.UI;

public class AnubisController : MonoBehaviour {
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Button[] healthButtons; // Массив кнопок UI для отображения жизней
    [SerializeField] private GameObject _winScreen;

    private Transform player;
    private float lastFireTime;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastFireTime = -fireRate;
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateHealthUI();
    }

    private void Update() {
        if (player == null) return;

        if (Vector3.Distance(transform.position, player.position) <= detectionRadius) {
            LookAtPlayer();

            if (Time.time - lastFireTime >= fireRate) {
                Fire();
                lastFireTime = Time.time;
            }
        }
    }

    private void LookAtPlayer() {
        bool isPlayerLeft = player.position.x < transform.position.x;
        spriteRenderer.flipX = !isPlayerLeft;
    }

    private void Fire() {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        float projectileSpeed = 30f;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2((spriteRenderer.flipX ? 1 : -1) * projectileSpeed, 0);

        Destroy(projectile, 2f);
    }

    public void TakeDamage() {
        currentHealth--;
        UpdateHealthUI();
        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        // Логика при уничтожении Анубиса
        _winScreen.SetActive(true);
        Destroy(gameObject);
    }

    private void UpdateHealthUI() {
        for (int i = 0; i < healthButtons.Length; i++) {
            healthButtons[i].interactable = i < currentHealth;
        }
    }
}
