using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField] private GameObject _vfx;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
    private void Start(){
        audioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            audioSource.Play();
            Instantiate(_vfx, transform.position, Quaternion.identity);
            Debug.Log("HIT");
            // Нанести урон игроку
            collision.gameObject.GetComponent<PlayerController>()?.TakeDamage();
            collision.gameObject.GetComponent<AnubisPlayerController>()?.TakeDamage();
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(DestroyAfterTimer());
        }
    }

    private IEnumerator DestroyAfterTimer(){
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
