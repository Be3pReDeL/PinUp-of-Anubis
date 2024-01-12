using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Letter : MonoBehaviour {
    [SerializeField] private GameObject coinPrefab; // Префаб монеты
    [SerializeField] private GameObject[] enemyPrefabs; // Массив префабов врагов

    private AudioSource audioSource;
    [SerializeField] private GameObject _vfx;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PlayerProjectile")) {
            Destroy(collision.gameObject);
            SpawnRandom();
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(DestroyAfterTimer());
        }
    }

    private void SpawnRandom() {
        audioSource.Play();
        Instantiate(_vfx, transform.position, Quaternion.identity);

        if (Random.value > 0.1f) {
            SpawnCoin();
        } else {
            SpawnEnemy();
        }
    }

    private void SpawnCoin() {
        Instantiate(coinPrefab, transform.position, Quaternion.identity); // Создаем монету на месте буквы
    }

    private void SpawnEnemy() {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[randomIndex], transform.position, Quaternion.identity); // Создаем случайного врага на месте буквы
    }

    private IEnumerator DestroyAfterTimer(){
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
