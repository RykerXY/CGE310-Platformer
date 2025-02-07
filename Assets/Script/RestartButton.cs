using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // ใส่ชื่อ Scene ใน Inspector
    public HealtSystem healtSystem;
    private CanvasGroup canvasGroup;
    private int health;
    private float fadeDuration = 1.5f; // ระยะเวลา fade in

    void Start()
    {
        health = healtSystem.getHealth();
        healtSystem = FindAnyObjectByType<HealtSystem>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; // เริ่มต้นให้โปร่งใส
        gameObject.SetActive(false);
    }

    void Update()
    {
        health = healtSystem.getHealth();
        if (health <= 0)
        {
            StartCoroutine(FadeIn());
        }
    }

    public void restart()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is not set in the Inspector!");
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null;
        }

        // ให้แน่ใจว่า alpha = 1 เมื่อจบ
        canvasGroup.alpha = 1f;
    }
}
