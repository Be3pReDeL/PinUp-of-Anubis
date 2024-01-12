using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField] private GameObject _vfx;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            audioSource.Play();
            Instantiate(_vfx, transform.position, Quaternion.identity);
            CoinsManager.ChangeCoinsAmount(1); // Добавляем одну монету
            GameManager.Instance.AddCoins(1); // Добавляем монету в подсчет текущей игры
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(DestroyAfterTimer());
        }
    }
    private IEnumerator DestroyAfterTimer(){
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
