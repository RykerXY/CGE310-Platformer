using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    private Vector2 lastCheckpointPos;
    
    private void Start()
    {
        // โหลดตำแหน่ง Checkpoint ล่าสุด ถ้ามี
        if (PlayerPrefs.HasKey("CheckpointX") && PlayerPrefs.HasKey("CheckpointY"))
        {
            lastCheckpointPos = new Vector2(
                PlayerPrefs.GetFloat("CheckpointX"),
                PlayerPrefs.GetFloat("CheckpointY")
            );

            // ย้ายผู้เล่นไปยังจุด Checkpoint ล่าสุด
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = lastCheckpointPos;
            }
        }
    }

    public void SaveCheckpoint(Vector2 checkpointPosition)
    {
        lastCheckpointPos = checkpointPosition;
        PlayerPrefs.SetFloat("CheckpointX", checkpointPosition.x);
        PlayerPrefs.SetFloat("CheckpointY", checkpointPosition.y);
        PlayerPrefs.Save(); // บันทึกข้อมูลลงหน่วยความจำ
    }
}
