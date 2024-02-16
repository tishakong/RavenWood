using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    GameObject scanObject;
    public float ObjectDistance;

    public PlayerMove playerMove;

    //public List<Transform> fixedPositions;  // 카메라 고정시킬 위치 저장 리스트
    //private int currentPositionInedex = 0;  // 현재 사용 중인 위치의 인덱스

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                scanObject = hit.collider.gameObject;

                // 오브젝트와 플레이어 사이의 거리
                float distance = Vector3.Distance(transform.position, scanObject.transform.position);

                if (distance < ObjectDistance)
                {
                    if (scanObject.CompareTag("Zoom"))
                    {
                        SwitchCameraPosition();
                    }
                }
            }
        }
    }

    private void SwitchCameraPosition()
    {
        ObjectData objData = scanObject.GetComponent<ObjectData>();

        if (objData != null && objData.isZoom)
        {
            transform.position = objData.savedPosition;
            playerMove.DisableRotation();
        }
    }
}
