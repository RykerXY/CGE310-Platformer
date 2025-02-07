using System.Threading;
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
    private bool onLadder;
    private int health;
    private bool onLadderClimb;
    
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer spriteRenderer;
    private HealtSystem healtSystem;
    private bool onPlatform = false;
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;

    private Animator animator;
    private float Timer;
    private LadderClimb ladderClimb;
    public float throwCooldown = 1f; 
    private float throwCooldownTimer = 0f; 
    private bool isDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healtSystem = GetComponent<HealtSystem>();
        animator = GetComponent<Animator>();
        ladderClimb = GetComponent<LadderClimb>();
    }

    // Update is called once per frame
    void Update()
    {
        onLadderClimb = ladderClimb.getOnLadder();
        health = healtSystem.getHealth();
        if (health > 0)
        {
            Move();
            Jump();
            flipSprite();
        }
        // Ground Check: ตรวจสอบว่ามีการแตะพื้น (Layer "Ground")
        isGrounded = Physics2D.IsTouchingLayers(capsuleCollider, LayerMask.GetMask("Ground"));
        
        // Attack
        if (throwCooldownTimer > 0f)
        {
            throwCooldownTimer -= Time.deltaTime; // Decrease the cooldown timer over time
            animator.SetBool("isAttacking", false);
        }

        if (Input.GetKeyDown(KeyCode.F) && throwCooldownTimer <= 0f && isGrounded)
        {
            Throw();
            throwCooldownTimer = throwCooldown; // Reset the cooldown timer
        }


        if(isGrounded)
        {
            onLadder = false;
            Timer += Time.deltaTime;
            if (Timer >= 0.2f)
            {
                animator.SetBool("isJumping", false);
                Timer = 0;
            }
            animator.SetBool("isDoubleJumping", false);

        }
        if(onLadderClimb)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isDoubleJumping", false);
        }
        //Debug.Log(isGrounded);
    }

    void FixedUpdate()
    {
        if (onPlatform && currentPlatform != null)
        {
            Vector3 platformDelta = currentPlatform.position - lastPlatformPosition;
            rb.position += (Vector2)platformDelta;
            lastPlatformPosition = currentPlatform.position;
        }
    }

    private void Move() {
        if (defaultMoveSpeed == 0) defaultMoveSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && !onLadder) {
            moveSpeed = sprintSpeed;
        } else if (!onLadder) {
            moveSpeed = defaultMoveSpeed;
        }

        if (Input.GetButton("Horizontal") && !onLadder) {
            float moveInput = Input.GetAxisRaw("Horizontal");
            Vector2 movement = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
            rb.linearVelocity = movement;
            animator.SetBool("isMoving", true);
        } 
        else if (onLadder) {
            float vertical = Input.GetAxisRaw("Vertical");
            rb.linearVelocity = new Vector2(0, vertical * (moveSpeed * 0.5f)); // เคลื่อนที่ขึ้นลงช้าลง
        }
        else {
            animator.SetBool("isMoving", false);
        }
    }

    
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower); 
            canDoubleJump = true;
            animator.SetBool("isJumping", true);
            Debug.Log(animator.GetBool("isJumping"));
        }
        else if (!isGrounded && canDoubleJump && Input.GetButtonDown("Jump"))
        {
            float moveDirection = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(moveDirection * moveSpeed, jumpPower);
            canDoubleJump = false;
            animator.SetBool("isDoubleJumping", true);
        }
    }

    private void Throw()
    {
        animator.SetBool("isAttacking", true);

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
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            onPlatform = true;
            currentPlatform = collision.gameObject.transform;
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
