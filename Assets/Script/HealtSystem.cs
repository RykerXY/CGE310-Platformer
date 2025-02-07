using UnityEngine;

public class HealtSystem : MonoBehaviour
{
    public int health = 4;
    public GameObject heal_1;
    public GameObject heal_2;
    public GameObject heal_3;
    public GameObject heal_4;
    public GameObject reStartButton;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 3){
            destroySprite(heal_4);
        }
        if (health == 2){
            destroySprite(heal_3);
        }
        if (health == 1){
            destroySprite(heal_2);
        }
        if (health == 0){
            destroySprite(heal_1);
        }
        //GameOver
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            Debug.Log("Dead");
            reStartButton.SetActive(true);
        }
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
        //Damage
        if(collision.gameObject.tag == "Enemy")
        {
            health -= 1;
        }
    }
    void destroySprite(GameObject gameObject)
    {
        Destroy(gameObject);
    }
    public int getHealth()
    {
        return health;
    }
    public void decreeseHealth()
    {
        health -= 1;
    }
}
