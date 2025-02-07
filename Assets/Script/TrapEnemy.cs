using UnityEngine;

public class TrapEnemy : MonoBehaviour
{
    private Animator animator;
    public HealtSystem healtSystem;

    void Start() {
        animator = GetComponent<Animator>();
        healtSystem = FindAnyObjectByType<HealtSystem>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Hit", true);
            healtSystem.decreeseHealth();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Hit", false);
        }
    }
}
