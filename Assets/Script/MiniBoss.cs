using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public float minSpeed = 2f;
    public float maxSpeed = 6f;
    public float jumpForce = 5f;
    public float jumpInterval = 4f;
    public float attackInterval = 3f;
    public float directionChangeInterval = 5f;
    public int maxHealth = 100;
    public int damage = 20;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private int currentHealth;
    private Rigidbody2D rb;
    private float nextJumpTime;
    private float nextAttackTime;
    private float nextDirectionChangeTime;
    private float movementDirection = 1;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        nextJumpTime = Time.time + jumpInterval;
        nextAttackTime = Time.time + attackInterval;
        nextDirectionChangeTime = Time.time + directionChangeInterval;
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        MovePattern();
        HandleJumping();
        HandleAttacks();
    }

    void MovePattern()
    {
        if (Time.time >= nextDirectionChangeTime)
        {
            movementDirection *= -1;
            currentSpeed = Random.Range(minSpeed, maxSpeed);
            nextDirectionChangeTime = Time.time + directionChangeInterval;
        }
        rb.linearVelocity = new Vector2(currentSpeed * movementDirection, rb.linearVelocity.y);
    }

    void HandleJumping()
    {
        if (Time.time >= nextJumpTime)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            nextJumpTime = Time.time + jumpInterval;
        }
    }

    void HandleAttacks()
    {
        if (Time.time >= nextAttackTime)
        {
            ThrowProjectile();
            nextAttackTime = Time.time + attackInterval;
        }
    }

    void ThrowProjectile()
    {
        if (projectilePrefab && firePoint)
        {
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
