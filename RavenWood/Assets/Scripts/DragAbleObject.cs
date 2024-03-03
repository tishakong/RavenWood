using UnityEngine;
using System.Collections;

public class DragAbleObject : MonoBehaviour
{
    Vector3 mousePosition;
    bool isDragging = false;
    float clickTime = 0f;
    float dragDelay = 0.5f; // 움직이기 위한 최소 클릭 시간
    Vector3 startPosition;
    new Rigidbody rigidbody;
    new Collider collider;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
        clickTime = Time.time;
        startPosition = transform.position; // 시작 포지션 기억
    }

    private void OnMouseDrag()
    {
        // 일정 시간 이상 누르고 있는지 확인
        if (!isDragging && Time.time - clickTime > dragDelay)
        {
            isDragging = true;
            StartDragging();
        }

        if (isDragging)
        {
            // 충돌 여부를 검사하여 이동을 멈춤
            if (isColliding())
            {
                return;
            }

            // 충돌이 없으면 이동 가능하므로 위치 변경
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        StartCoroutine(EndDraggingWithDelay());
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    void StartDragging()
    {
        rigidbody.mass = 0.1f;
        rigidbody.useGravity = false;
    }

    IEnumerator EndDraggingWithDelay()
    {
        rigidbody.mass = 5f;
        rigidbody.useGravity = true;

        yield return new WaitForSeconds(1f);

        if (transform.position.y < 0)
        {
            transform.position = startPosition;
        }
    }

    bool isColliding()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, collider.bounds.extents);
        foreach (Collider col in colliders)
        {
            if (col.tag == "Room" && col != collider && isDragging)
            {
                Vector3 direction = col.ClosestPoint(transform.position) - transform.position;
                Debug.Log("Collided with object at direction: " + direction.normalized);

                // 충돌한 방향의 반대 방향으로 힘을 가하기 위해 방향을 반전시킴
                Vector3 forceDirection = -direction.normalized;

                isDragging = false;

                // 힘을 가할 정도를 조절할 수 있도록 설정 (이 예제에서는 10의 힘을 가함)
                float forceMagnitude = 10f;

                // Rigidbody에 힘을 가함
                rigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);

                // 충돌한 위치로 이동시키지 않음
                return false;
            }
        }
        return false;
    }
}
