using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public float climbSpeed = 3f; // ความเร็วในการปีน
    public float gravityScale = 3f;
    private bool isClimbing = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isClimbing)
        {
            float vertical = Input.GetAxisRaw("Vertical"); // รับค่าปุ่มขึ้น-ลง
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
            rb.gravityScale = 0; // ปิดแรงโน้มถ่วงระหว่างปีน
        }
        else
        {
            rb.gravityScale = gravityScale; // คืนค่าแรงโน้มถ่วงปกติ
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }
}
