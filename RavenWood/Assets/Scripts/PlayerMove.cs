using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;

    public float walkSpeed;
    public float lookSensitivity;
    public float cameraRotationLimit;
    private float currentCameraRotationX = 0;
    public Camera theCamera;

    GameObject scanObject;
    public GameManager manager;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        CameraRotation();
        CharacterRotation();

        // Scan Object & Action
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                scanObject = hit.collider.gameObject;
                manager.Action(scanObject);
            }
            else
            {
                scanObject = null;
            }
        }
    }

    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * walkSpeed;

        rigid.MovePosition(transform.position + velocity * Time.deltaTime);

    }

    private void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(characterRotationY));
    }

    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity;
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        
    
    }
}
