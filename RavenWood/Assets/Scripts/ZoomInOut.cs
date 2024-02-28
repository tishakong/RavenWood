using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    GameObject scanObject;
    GameObject zoomObject;
    public Quaternion currentRotation;
    public float ObjectDistance;
    public bool ZoomIn = false;
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

                        zoomObject = scanObject;

                        currentRotation = transform.rotation;
                        SwitchCameraPosition();
                        transform.SetParent(null);
                        if (data.isPanel)
                        {
                            Panel[data.panelNum - 1].SetActive(true);
                        }
                        backButton.SetActive(true);
                        scanObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void SwitchCameraPosition()
    {
        playerMove.DisableRotation();

        ObjectData objData = scanObject.GetComponent<ObjectData>();

        if (objData != null && objData.isZoom)
        {
            transform.position = objData.savedPosition;

            Quaternion newRotation = Quaternion.identity * objData.savedRotation;

            transform.rotation = newRotation;
        }
    }


    public void CurrentCameraPosition()
    {

        Debug.Log(zoomObject);
        ObjectData data = zoomObject.GetComponent<ObjectData>();

        if (data.isPanel){
            Panel[data.panelNum - 1].SetActive(false);
        }
        backButton.SetActive(false);
        playerMove.EnableRotation();
        transform.SetParent(playerMove.transform);
        transform.rotation = currentRotation;
        transform.localPosition = new Vector3(0f, 0.6f, 0f);
        zoomObject.SetActive(true);

    }
}
