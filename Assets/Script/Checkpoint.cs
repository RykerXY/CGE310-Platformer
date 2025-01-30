using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ตรวจจับเฉพาะ Player
        {
            Debug.Log("Player reached the checkpoint.");
            CheckpointSystem checkpointSystem = FindAnyObjectByType<CheckpointSystem>();
            if (checkpointSystem != null)
            {
                Debug.Log("Save checkpoint.");
                checkpointSystem.SaveCheckpoint(transform.position);
            }
        }
    }
}
