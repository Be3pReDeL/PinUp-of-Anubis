using UnityEngine;

public class PlatformController : MonoBehaviour {
    [SerializeField] private AudioClip landingSound; // Звук при приземлении на платформу
    [SerializeField] private GameObject vfxPrefab; // Префаб визуального эффекта
    [SerializeField] private Transform vfxSpawnPoint; // Точка, где будет создан VFX

    private AudioSource audioSource;

    private void Awake() {
        // Проверяем, есть ли AudioSource на платформе, иначе добавляем его
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayLandingSound();
            SpawnVFX();
        }
    }

    private void PlayLandingSound() {
        if (landingSound != null) {
            audioSource.PlayOneShot(landingSound);
        }
    }

    private void SpawnVFX() {
        if (vfxPrefab != null) {
            Instantiate(vfxPrefab, vfxSpawnPoint.position, Quaternion.identity);
        }
    }
}
