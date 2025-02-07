using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Sprite defaultButton;
    public Sprite pressedButton;
    public float SpriteScale;
    public float activationDistance = 2.0f; // ระยะทางที่ผู้เล่นต้องอยู่ใกล้เพื่อกดปุ่ม
    private int checker = 0;
    private bool isBoxNear = false;
    private bool isActive = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(SpriteScale, SpriteScale, 1.0f);
    }

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
            spriteRenderer.sprite = pressedButton;
            checker++;
            Debug.Log("Box is near");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // ตรวจสอบว่าผู้เล่นออกจากพื้นที่ Trigger หรือไม่
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            checker--;
            if (checker == 0) {
            isBoxNear = false;
            isActive = false;
            spriteRenderer.sprite = defaultButton;
            Debug.Log("Box has left");
            }
        }
    }

    public bool ActivateButton()
    {
        // ส่งคำสั่งให้แพลตฟอร์มเริ่มทำงาน
        return isActive;
    }
}
