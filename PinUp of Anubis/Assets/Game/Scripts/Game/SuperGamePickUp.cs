using System.Collections;
using UnityEngine;

public class SuperGamePickUp : MonoBehaviour {
    [SerializeField] private GameObject superGameMenu; // Меню "супер игры"

    private AudioSource audioSource;
    [SerializeField] private GameObject _vfx;

    private void Awake(){
        superGameMenu = GameObject.FindGameObjectWithTag("SuperGame");

        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            audioSource.Play();
            Instantiate(_vfx, transform.position, Quaternion.identity);
            ActivateSuperGame();
        }
    }

    private void ActivateSuperGame() {
        if (superGameMenu != null) {
            superGameMenu.transform.GetChild(0).gameObject.SetActive(true); // Активация меню "супер игры"

            Time.timeScale = 0;
        }

        // Опционально: уничтожаем объект после активации
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterTimer(){
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
