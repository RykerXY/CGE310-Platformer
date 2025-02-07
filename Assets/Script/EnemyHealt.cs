using UnityEngine;

public class EnemyHealt : MonoBehaviour
{
    public int health = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Attack")) {
            health -= 1;
        }
    }
    void Update() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
