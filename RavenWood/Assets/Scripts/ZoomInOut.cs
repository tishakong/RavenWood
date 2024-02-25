using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    GameObject scanObject;
    public Quaternion currentRotation;
    public float ObjectDistance;
    public GameObject potionPanel;
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

                // 오브젝트와 플레이어 사이의 거리
                float distance = Vector3.Distance(transform.position, scanObject.transform.position);

                if (distance < ObjectDistance)
                {
                    if (scanObject.CompareTag("Zoom"))
                    {
                        currentRotation = transform.rotation;
                        SwitchCameraPosition();
                        transform.SetParent(null);
                        potionPanel.SetActive(true);
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
            transform.rotation = objData.savedRotation;
            playerMove.DisableRotation();
        }
    }

    public void CurrentCameraPosition()
    {
        potionPanel.SetActive(false);
        backButton.SetActive(false);
        playerMove.EnableRotation();
        transform.SetParent(playerMove.transform);
        transform.rotation = currentRotation;
        transform.localPosition = new Vector3(0f, 0.6f, 0f);
    }
}
