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
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ground Check
        isGrounded = Physics2D.IsTouchingLayers(capsuleCollider, LayerMask.GetMask("Ground"));
        
        Move();
        Jump();
        flipSprite();

        //Attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            Throw();
        }
    }

    private void Move()
    {
        //Sprint
        if(defaultMoveSpeed == 0) defaultMoveSpeed = moveSpeed;       
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
        }

        //Move
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = movement;
        
    }
    
    private void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            canDoubleJump = true;
        }
        if (!isGrounded && canDoubleJump && Input.GetButtonDown("Jump"))
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

        if(spriteRenderer.flipX == true)
        {
            direction = transform.right * -1f;
        }
        if (attackSpriteRb != null)
        {
            attackSpriteRb.AddForce(direction * throwForce, ForceMode2D.Impulse);
        }
    }
    private void flipSprite()
    {
        if(Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
        }
        if(Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
        }
    }
}
