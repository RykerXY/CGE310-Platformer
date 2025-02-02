using UnityEngine;

public class PlayerMovingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 platformVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // เก็บความเร็วของ platform
            platformVelocity = collision.relativeVelocity;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // เมื่อออกจาก platform
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            platformVelocity = Vector2.zero; // หยุดการเคลื่อนที่ตาม platform
        }
    }

    void Update()
    {
        // รับ Input ของผู้เล่น (การเดิน)
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * 5f, rb.linearVelocity.y); // 5f คือความเร็วของ player

        // ถ้า player ยืนบน platform, ให้เคลื่อนที่ตาม platform
        if (platformVelocity != Vector2.zero)
        {
            // เพิ่มความเร็วของ platform ลงในความเร็วของ player
            rb.linearVelocity += new Vector2(platformVelocity.x, 0);
        }
    }
}
