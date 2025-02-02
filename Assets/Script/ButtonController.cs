using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public float activationDistance = 2.0f; // ระยะทางที่ผู้เล่นต้องอยู่ใกล้เพื่อกดปุ่ม

    private bool isBoxNear = false;
    private bool isActive = false;

    void Update()
    {
        // ตรวจสอบว่าผู้เล่นอยู่ใกล้ปุ่มหรือไม่
        if (isBoxNear)
        {
            // ส่งคำสั่งให้แพลตฟอร์มเริ่มทำงาน
            isActive = true;
        }  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่าผู้เล่นเข้ามาในพื้นที่ Trigger หรือไม่
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            isBoxNear = true;
            Debug.Log("Box is near");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // ตรวจสอบว่าผู้เล่นออกจากพื้นที่ Trigger หรือไม่
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            isBoxNear = false;
            isActive = false;
            Debug.Log("Box has left");
        }
    }

    public bool ActivateButton()
    {
        // ส่งคำสั่งให้แพลตฟอร์มเริ่มทำงาน
        return isActive;
    }
}
