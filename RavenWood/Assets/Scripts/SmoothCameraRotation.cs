using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SmoothCameraRotation : MonoBehaviour
{
    public Transform target; // 카메라가 바라볼 대상
    public float rotationSpeed = 1.0f; // 회전 속도
    private Quaternion targetRotation; // 목표 회전값

    void Start()
    {
        GameObject targetObject = GameObject.Find("TargetPlane"); // 대상의 GameObject를 찾습니다.
        if (targetObject != null)
        {
            target = targetObject.transform; // 대상의 Transform을 할당합니다.
        }
        else
        {
            Debug.LogError("Target GameObject not found!"); // 대상을 찾지 못한 경우 에러를 출력합니다.
        }

        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (target != null)
        {
            // 대상을 바라보는 방향 벡터를 구합니다.
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0f; // y축 방향은 회전하지 않도록 합니다.

            // 목표 회전값을 대상을 향하는 방향으로 설정합니다.
            targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        }

        // 현재 회전값과 목표 회전값을 선형 보간합니다.
        Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 카메라의 회전값을 업데이트합니다.
        transform.rotation = newRotation;
    }
}
