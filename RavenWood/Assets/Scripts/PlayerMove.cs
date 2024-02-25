using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;


    public float walkSpeed;
    public float lookSensitivity;
    public float cameraRotationLimit;
    private float currentCameraRotationX = 0;
    public Camera theCamera;

    public float ObjectDistance;      // 코드를 비활성화할지 여부
    private bool rotationEnabled = true;

    GameObject scanObject;
    public GameManager manager;

    public Animator[] doorAnimators;  // 여러 개의 문을 저장할 배열
    bool[] isOpenArray;               // 각 문의 상태를 저장하는 배열

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        isOpenArray = new bool[doorAnimators.Length];
    }

    void FixedUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3 (h,0, v),ForceMode.Impulse);

        Move();
        CameraRotation();
        CharacterRotation();
    }


    private void Update()
    {
        // Scan Object & Action
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
                    if (manager.isAction)
                    {
                        scanObject = null;
                    }
                    else
                    {
                        // 문 열기
                        if (scanObject.CompareTag("Door"))
                        {

                            Animator doorAnimator = scanObject.GetComponentInParent<Animator>();

                            if (doorAnimator != null)
                            {
                                OpenOrCloseDoor(doorAnimator);
                            }

                        }
                        // 오브젝트 상태창 띄우기
                        else if (scanObject.CompareTag("Object"))
                        {
                            manager.Action(scanObject);

                        }
                        // 획득 가능 오브젝트
                        else if (scanObject.CompareTag("ObtainableObject"))
                        {

                            if (scanObject.name == "Inventory")
                            {
                                Destroy(scanObject);
                                manager.ShowInventory();
                            }

                        }
                    }

                }
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
        if (rotationEnabled)
        {
            float yRotation = Input.GetAxisRaw("Mouse X");
            Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;
            rigid.MoveRotation(rigid.rotation * Quaternion.Euler(characterRotationY));
        }
    }

    private void CameraRotation()
    {
        if (rotationEnabled)
        {
            float xRotation = Input.GetAxisRaw("Mouse Y");
            float cameraRotationX = xRotation * lookSensitivity;
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

    // 비활성화하는 함수
    public void DisableRotation()
    {
        rotationEnabled = false;
    }

    // 활성화하는 함수
    public void EnableRotation()
    {
        rotationEnabled = true;
    }


    // 문 여는 함수

    void OpenOrCloseDoor(Animator doorAnimator)
    {
        int doorIndex = System.Array.IndexOf(doorAnimators, doorAnimator);

        if (doorIndex != -1) // 해당하는 문을 찾았을 경우
        {
            if (isOpenArray[doorIndex])
            {
                doorAnimator.SetBool("isOpen", false);
                isOpenArray[doorIndex] = false;
            }
            else
            {
                doorAnimator.SetBool("isOpen", true);
                isOpenArray[doorIndex] = true;
            }
        }
    }
}
