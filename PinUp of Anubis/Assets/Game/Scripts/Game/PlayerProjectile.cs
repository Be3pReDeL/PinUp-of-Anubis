using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField] private GameObject _vfx;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
    private void Start(){
        audioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            
            Instantiate(_vfx, transform.position, Quaternion.identity);
            // Нанести урон врагу
            collision.gameObject.GetComponent<EnemyController>()?.TakeDamage();
            collision.gameObject.GetComponent<AnubisController>()?.TakeDamage();
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(DestroyAfterTimer());
        }
    }

    private IEnumerator DestroyAfterTimer(){
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
