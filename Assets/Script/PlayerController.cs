using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject attackPrefab;
    public float throwForce = 30f;
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpPower = 10f;

    private bool isGrounded;
    private bool canDoubleJump;
    private float defaultMoveSpeed;
    private int health;
    
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer spriteRenderer;
    private HealtSystem healtSystem;

    // --- ตัวแปรสำหรับติดตาม moving platform ---
    private bool onPlatform = false;
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healtSystem = GetComponent<HealtSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        health = healtSystem.getHealth();
        if (!(health <= 0))
        {
            Move();
            Jump();
            flipSprite();
        }
        // Ground Check: ตรวจสอบว่ามีการแตะพื้น (Layer "Ground")
        isGrounded = Physics2D.IsTouchingLayers(capsuleCollider, LayerMask.GetMask("Ground"));
        
        // Attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            Throw();
        }
    }

    // FixedUpdate สำหรับการอัปเดตฟิสิกส์และคำนวณ offset ของ platform
    void FixedUpdate()
    {
        // ถ้า player อยู่บน moving platform ให้นำการเปลี่ยนแปลงตำแหน่ง (delta) ของ platform มาบวกกับตำแหน่ง player
        if (onPlatform && currentPlatform != null)
        {
            Vector3 platformDelta = currentPlatform.position - lastPlatformPosition;
            rb.position += (Vector2)platformDelta;
            lastPlatformPosition = currentPlatform.position;
        }
    }

    private void Move()
    {
        // Sprint
        if (defaultMoveSpeed == 0) defaultMoveSpeed = moveSpeed;       
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
        }

        // รับ input การเคลื่อนที่แนวนอน
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = movement;
    }
    
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            canDoubleJump = true;
        }
        else if (!isGrounded && canDoubleJump && Input.GetButtonDown("Jump"))
        {
            float moveDirection = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(moveDirection * moveSpeed, jumpPower);
            canDoubleJump = false;
        }
    }

    private void Throw()
    {
        GameObject attackSprite = Instantiate(attackPrefab, transform.position, transform.rotation);
        Rigidbody2D attackSpriteRb = attackSprite.GetComponent<Rigidbody2D>();
        Vector2 direction = transform.right; // ใช้ right หรือ left ขึ้นอยู่กับทิศทางของผู้เล่น

        if (spriteRenderer.flipX)
        {
            direction = -transform.right;
        }
        if (attackSpriteRb != null)
        {
            attackSpriteRb.AddForce(direction * throwForce, ForceMode2D.Impulse);
        }
    }

    private void flipSprite()
    {
        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
        }
    }

    // --- ส่วนของการตรวจจับ moving platform ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจจับเฉพาะวัตถุที่มี tag "MovingPlatform"
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            onPlatform = true;
            currentPlatform = collision.transform;
            lastPlatformPosition = currentPlatform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            onPlatform = false;
            currentPlatform = null;
        }
    }
}
