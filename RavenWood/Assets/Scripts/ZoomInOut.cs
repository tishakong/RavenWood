using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    GameObject scanObject;
    GameObject zoomObject;
    Quaternion currentRotation;
    public float ObjectDistance;
    public bool ZoomIn = false;
    public GameObject[] Panel;
    public GameObject backButton;
    public GameObject mixButton;

    public PlayerMove playerMove;
    public GameManager gameManager;

    Collider PotionsCollider;

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
                        ZoomIn = true;

                        gameManager.DelayedHidePanel();

                        if (zoomObject.name == "Pot")
                        {
                            zoomObject.tag = "InteractiveObject";
                            mixButton.SetActive(true);
                            scanObject.SetActive(true);
                        }
                        else if (zoomObject.name == "Potions")
                        {
                            scanObject.SetActive(true);
                            PotionsCollider = scanObject.GetComponent<Collider>();
                            PotionsCollider.enabled = !PotionsCollider.enabled;
                            StartCoroutine(ChangePotionTag(zoomObject));
                        }
                    }
                }
            }
        }
    }

    IEnumerator ChangePotionTag(GameObject zoomObject)
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < zoomObject.transform.childCount; i++)
        {
            Transform child = zoomObject.transform.GetChild(i);
            child.gameObject.tag = "ObtainableObject";
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
            transform.rotation = Quaternion.identity * newRotation;
        }
    }


    public void CurrentCameraPosition()
    {

        Debug.Log(zoomObject);
        ObjectData data = zoomObject.GetComponent<ObjectData>();

        if (data.isPanel){
            Panel[data.panelNum - 1].SetActive(false);
        }

        if (zoomObject.name == "Pot")
        {
            zoomObject.tag = "Zoom";
            mixButton.SetActive(false);
        }
        else if (zoomObject.name == "Potions")
        {
            PotionsCollider.enabled = !PotionsCollider.enabled;
            for (int i = 0; i < zoomObject.transform.childCount; i++)
            {
                Transform child = zoomObject.transform.GetChild(i);
                child.gameObject.tag = "Untagged";
            }
        }

        backButton.SetActive(false);
        playerMove.EnableRotation();
        zoomObject.SetActive(true);

        ZoomOut();
    }

    public void ZoomOut()
    {
        transform.SetParent(playerMove.transform);
        transform.rotation = currentRotation;
        transform.localPosition = new Vector3(0f, 0.6f, 0f);
        ZoomIn = false;
    }
}
