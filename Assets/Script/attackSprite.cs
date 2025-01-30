using UnityEngine;

public class attackSprite : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
            Debug.Log("hit");
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
