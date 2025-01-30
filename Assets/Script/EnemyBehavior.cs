using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public bool LeftRight = false;
    public bool UpDown = false;
    public bool JumpingEnemy = false;
    private Rigidbody2D rb;
    public float jumpPower = 5f;
    public float repeatTime = 2f;
    private float timer;
    public float moveDistance = 2f; // ระยะทางที่วัตถุจะเคลื่อนที่ไปทางซ้ายและขวา
    public float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่

    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position; // บันทึกตำแหน่งเริ่มต้น 
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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(JumpingEnemy)
        {
            timer += Time.deltaTime;
            if (timer >= repeatTime)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                timer = 0f;
            }
        }
    }
}
