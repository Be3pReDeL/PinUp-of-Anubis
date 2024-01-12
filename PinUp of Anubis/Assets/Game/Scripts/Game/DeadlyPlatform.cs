using UnityEngine;

public class DeadlyPlatform : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null) {
                for(int i = 0; i < 3; i++)
                    player.TakeDamage();
            }
        }
    }
}
