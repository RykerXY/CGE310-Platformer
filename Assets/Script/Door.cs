using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isActive = false;
    public ButtonController buttonController;
    public float speed = 2.0f; // ความเร็วของการเคลื่อนที่
    public float distance = 5.0f; // ระยะทางที่แพลตฟอร์มจะเคลื่อนที่
    public float targetYPosition = -5.0f;

    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position; // บันทึกตำแหน่งเริ่มต้น
    }
    void Update()
    {
        isActive = buttonController.ActivateButton();
        
        if (isActive)
        {
            // เคลื่อนที่ลงไปที่ตำแหน่ง targetYPosition
            MovePlatform(targetYPosition);
        }
        else
        {
            // เคลื่อนที่กลับขึ้นไปที่ตำแหน่งเริ่มต้น (y = 0)
            MovePlatform(startPosition.y);
        }
    }

    // ฟังก์ชันสำหรับเคลื่อนที่แพลตฟอร์ม
    private void MovePlatform(float targetY)
    {
        Vector3 targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}