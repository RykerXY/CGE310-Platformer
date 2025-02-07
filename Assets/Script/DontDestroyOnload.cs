using UnityEngine;

public class DontDestroyOnload : MonoBehaviour
{
    private static DontDestroyOnload instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
