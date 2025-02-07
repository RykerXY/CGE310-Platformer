using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool onLadder = false;
    private float climbSpeed = 5f;
    private PlayerController playerController;
    private Animator animator;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        float vertical = Input.GetAxisRaw("Vertical"); // รับค่ากดขึ้น/ลง
        if (onLadder) {
            rb.linearVelocity = new Vector2(0, vertical * climbSpeed); // ไม่ให้ขยับซ้าย/ขวา
            rb.gravityScale = 0; // ปิดแรงโน้มถ่วงขณะอยู่บนบันได
        }
        else {
            // ถ้าไม่อยู่บนบันได ให้สามารถขยับได้ปกติ
            float horizontal = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(horizontal * playerController.moveSpeed, rb.linearVelocity.y); // เคลื่อนที่แนวนอน
        }
        if (vertical == 0 && onLadder) {
                animator.speed = 0; // หยุด animation เมื่อผู้เล่นหยุดปีน
            } else {
                animator.speed = 1; // เริ่มเล่น animation ใหม่เมื่อปีนขึ้น/ลง
            }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ladder")) {
            animator.SetBool("isClimbing", true);

            onLadder = true;
            rb.gravityScale = 0; // ปิดแรงโน้มถ่วงเมื่อเริ่มปีนบันได
            rb.linearVelocity = Vector2.zero; // หยุดความเร็วแนวตั้งที่ค้างอยู่
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Ladder")) {
            animator.SetBool("isClimbing", false);

            onLadder = false;
            rb.gravityScale = 4; // เปิดแรงโน้มถ่วงเมื่อออกจากบันได
        }
    }
    public bool getOnLadder() {
        return onLadder;
    }
}
