using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool LeftRightMove = false;
    [SerializeField] private ButtonController buttonController;
    [SerializeField] private Transform targetPosition; // ตำแหน่งปลายทางที่แพลตฟอร์มจะเคลื่อนที่ไป
    public float speed = 2.0f; // ความเร็วของการเคลื่อนที่
    public float moveDistance = 2.0f;

    private Vector3 startPosition; // ตำแหน่งเริ่มต้นของแพลตฟอร์ม
    private bool isActive = false; // สถานะการทำงาน


    void Start()
    {
        startPosition = transform.position;
        if (!buttonController)
        {
            TryGetComponent(out buttonController);
        }
        if (!targetPosition)
    {
        TryGetComponent(out targetPosition);
    }
    }

    void Update()
    {
        isActive = buttonController?.ActivateButton() ?? false;

        if (LeftRightMove)
        {
            float offset = Mathf.Sin(Time.time * speed) * moveDistance;
            transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
        }
        else
        {
            if (isActive)
            {
                // เคลื่อนที่ไปยังตำแหน่งปลายทาง
                MovePlatform(targetPosition.position);
            }
            else
            {
                // เคลื่อนที่กลับไปยังตำแหน่งเริ่มต้น
                MovePlatform(startPosition);
            }
        }
    }

    // ฟังก์ชันสำหรับเคลื่อนที่แพลตฟอร์ม
    private void MovePlatform(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
