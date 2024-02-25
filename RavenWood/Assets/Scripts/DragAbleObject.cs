using UnityEngine;

public class DragAbleObject : MonoBehaviour
{
    Vector3 mousePosition;
    bool isDragging = false;
    float clickTime = 0f;
    float dragDelay = 0.5f; // 움직이기 위한 최소 클릭 시간
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
            // 마우스의 이동 방향으로 레이캐스트 수행
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 충돌한 객체의 Collider를 가져옴
                Collider hitCollider = hitInfo.collider;
                // 만약 충돌한 객체의 Collider가 자신의 Collider가 아니면 이동을 막음
                if (hitCollider != collider)
                    return;
            }

            // 충돌이 없으면 이동 가능하므로 위치 변경
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        EndDragging();
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    void StartDragging()
    {
        rigidbody.mass = 0.1f;
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void EndDragging()
    {
        rigidbody.mass = 5f;
        rigidbody.useGravity = true;
        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotation;
    }
}
