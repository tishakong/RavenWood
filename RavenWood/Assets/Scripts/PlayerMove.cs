using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    public GameObject scanObject;
    public GameObject selectedItem;

    public GameManager manager;
    public AudioManager audioManager;
    public InventoryManager inventoryManager;
    public Item item;
    public ZoomInOut zoom;
    public MakePotion makePotion;

    public Animator[] doorAnimators;  // 여러 개의 문을 저장할 배열
    bool[] isOpenArray;               // 각 문의 상태를 저장하는 배열

    public bool isPanel = false;      // 개별 패널 여는지 체크

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
        Move();

        if(Input.GetMouseButton(1)) {
            CameraRotation();
            CharacterRotation();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Outside"))
        {
            if (manager.isRecovery)
            {
                manager.GameClear();
            }
            else
            {
                manager.GameOver();
            }
        }
    }

    private void Update()
    {
        // Scan Object & Action
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject())
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
                        if (manager.isAction) //상태창이 떠있다면 scanObject 작동 X
                        {
                            scanObject = null;
                        }
                        else
                        {
                            if (selectedItem == null) //인벤토리 아이템 선택이 안되어 있을 경우
                            {
                                // 서랍장 열기
                                if (scanObject.CompareTag("Door") && !zoom.ZoomIn)
                                {
                                    DoorEvent();
                                }
                                // 오브젝트 상태창 띄우기
                                else if (scanObject.CompareTag("Object") || scanObject.CompareTag("InteractiveObject"))
                                {
                                    if (!zoom.ZoomIn)
                                    {
                                        manager.Action(scanObject);
                                    }
                                }
                                // 획득 가능 오브젝트
                                else if (scanObject.CompareTag("ObtainableObject"))
                                {
                                    ObtainableObjectEvent();
                                }
                                else if (scanObject.CompareTag("FinishedPotion"))
                                {
                                    scanObject.SetActive(false);
                                    manager.PotionEnding();
                                }
                            }
                            else //인벤토리 아이템이 선택되어 있을 경우
                            {
                                if (scanObject.CompareTag("InteractiveObject")) //인벤토리 아이템과 상호작용이 가능한 오브젝트를 클릭하였으면 해당하는 상호작용 실행하는 코드 작성
                                {
                                    item.UseItem(selectedItem, scanObject);
                                }
                                Image itemImage = selectedItem.transform.parent.GetComponent<Image>();
                                itemImage.color = Color.white;
                                selectedItem = null;
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

    public void DoorEvent()
    {
        Animator doorAnimator = scanObject.GetComponentInParent<Animator>();
        if (doorAnimator != null)
        {
            int doorIndex = System.Array.IndexOf(doorAnimators, doorAnimator);

            if (doorIndex != -1) // 해당하는 문을 찾았을 경우
            {
                if (isOpenArray[doorIndex])
                {
                    doorAnimator.SetBool("isOpen", false);
                    isOpenArray[doorIndex] = false;
                    print(doorAnimator.GetBool("isOpen"));
                }
                else
                {
                    doorAnimator.SetBool("isOpen", true);
                    isOpenArray[doorIndex] = true;
                    print(doorAnimator.GetBool("isOpen"));
                }
            }
        }
    }

    void ObtainableObjectEvent()
    {
        if (inventoryManager.isInventoryActivate)
        {
            inventoryManager.AddToInventory(scanObject.name);
            audioManager.PlaySound("GetItem");
            if (scanObject.name == "Tomato")
            {
                scanObject.tag = "Untagged";
                return;
            }
            Destroy(scanObject);
        }
        else
        {
            if (scanObject.name == "Inventory")
            {
                Destroy(scanObject);
                audioManager.PlaySound("GetItem");
                inventoryManager.ShowInventory();
            }
            else
            {
                manager.OutOfHands();
            }
        }
    }

    //클릭 위치에 UI가 있는지 확인하는 함수
    bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
