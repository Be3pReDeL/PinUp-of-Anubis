using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _fireRate = 2f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;

    private bool dead = false;

    private Transform _player;
    private float _lastFireTime;

    private AudioSource audioSource;
    [SerializeField] private GameObject _vfx;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _lastFireTime = -_fireRate; // Обеспечиваем возможность первой стрельбы сразу же
    }

    private IEnumerator DestroyAfterTimer(){
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }

    private void Update() {
        if(!dead){
            if (_player == null) return;

            // Проверяем, находится ли игрок в радиусе
            if (Vector3.Distance(transform.position, _player.position) <= _detectionRadius) {
                LookAtPlayer();

                // Стреляем, если прошло достаточно времени с последнего выстрела
                if (Time.time - _lastFireTime >= _fireRate) {
                    Fire();
                    _lastFireTime = Time.time;
                }
            }
        }
    }

    private void LookAtPlayer() {
        // Определяем, с какой стороны находится игрок
        bool isPlayerLeft = _player.position.x < transform.position.x;
        GetComponent<SpriteRenderer>().flipX = !isPlayerLeft;
    }

    private void Fire() {
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
        float projectileSpeed = 30f; // Увеличиваем скорость Projectile'ов
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2((GetComponent<SpriteRenderer>().flipX ? 1 : -1) * projectileSpeed, 0);

        Destroy(projectile, 2f); // Уничтожаем Projectile через 2 секунды
    }

    // Вызывается при попадании снаряда игрока
    public void TakeDamage() {
        dead = true;
        GameManager.Instance.EnemyDefeated(); // Сообщаем о победе над врагом
        audioSource.Play();
        Instantiate(_vfx, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(DestroyAfterTimer());
    }
}
