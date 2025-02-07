using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public bool LeftRight = false;
    public bool UpDown = false;
    public bool JumpingEnemy = false;
    private Rigidbody2D rb;
    public float jumpPower = 5f;
    public float repeatTime = 2f;
    private float Jumptime;
    public float moveDistance = 2f; // ระยะทางที่วัตถุจะเคลื่อนที่ไปทางซ้ายและขวา
    public float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่

    private Vector3 startPos;
    private SpriteRenderer spriteRenderer; // เพิ่ม SpriteRenderer เพื่อควบคุมการหันของ Sprite

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position; // บันทึกตำแหน่งเริ่มต้น
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // ดึง SpriteRenderer
    }

    // Update is called once per frame
    void Update()
    {
        LeftRightMove();
        UpDownMove();
        Jump();
    }

    void LeftRightMove()
    {
        if (LeftRight)
        {
            float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance; // คำนวณตำแหน่งตามฟังก์ชัน Sine
            transform.position = new Vector3(startPos.x + offset, startPos.y, startPos.z);
            //Debug.Log(offset);
            // ตรวจสอบทิศทางการเคลื่อนที่และหัน Sprite ตามทิศทาง
            if (offset > 1.5)
            {
                spriteRenderer.flipX = false; // หันไปทางขวา
            }
            else if (offset < -1.5)
            {
                spriteRenderer.flipX = true; // หันไปทางซ้าย
            }
        }
    }

    void UpDownMove()
    {
        if (UpDown)
        {
            float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance; // คำนวณตำแหน่งตามฟังก์ชัน Sine
            transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);

        }
    }

    private void Jump()
    {
        if (JumpingEnemy)
        {
            Jumptime += Time.deltaTime;
            //Debug.Log(Jumptime);
            if (Jumptime >= repeatTime)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                Jumptime = 0f;
            }
        }
    }
}