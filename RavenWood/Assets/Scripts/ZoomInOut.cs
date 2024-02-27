using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    GameObject scanObject;
    public Quaternion currentRotation;
    public float ObjectDistance;
    public GameObject[] Panel;
    public GameObject backButton;

    public PlayerMove playerMove;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                scanObject = hit.collider.gameObject;

                ObjectData data = scanObject.GetComponent<ObjectData>();

                // 오브젝트와 플레이어 사이의 거리
                float distance = Vector3.Distance(transform.position, scanObject.transform.position);

                if (distance < ObjectDistance)
                {
                    if (scanObject.CompareTag("Zoom"))
                    {
                        currentRotation = transform.rotation;
                        SwitchCameraPosition();
                        transform.SetParent(null);
                        if (data.panelNum != 0)
                        {
                            Panel[data.panelNum - 1].SetActive(true);
                        }
                        backButton.SetActive(true);
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

            // Quaternion.Slerp 대신 Quaternion.Euler 사용
            Quaternion newRotation = Quaternion.identity * objData.savedRotation;


            // 디버그 출력 추가
            Debug.Log("Transform Rotation: " + transform.rotation);
            Debug.Log("Saved Rotation " + objData.savedRotation);
            Debug.Log("New Rotation: " + newRotation);

            transform.rotation = newRotation;

            playerMove.DisableRotation();
        }
    }


    public void CurrentCameraPosition()
    {
        ObjectData data = scanObject.GetComponent<ObjectData>();

        if (data.panelNum != 0){
            Panel[data.panelNum - 1].SetActive(false);
        }
        backButton.SetActive(false);
        playerMove.EnableRotation();
        transform.SetParent(playerMove.transform);
        transform.rotation = currentRotation;
        transform.localPosition = new Vector3(0f, 0.6f, 0f);
    }
}
