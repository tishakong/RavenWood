using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
<<<<<<< Updated upstream
=======

    public float walkSpeed;
    public float lookSensitivity;
    public float cameraRotationLimit;
    private float currentCameraRotationX = 0;
    public Camera theCamera;
    public float ObjectDistance;

>>>>>>> Stashed changes
    GameObject scanObject;
    public GameManager manager;

    public Animator anim;
    bool isOpen = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
<<<<<<< Updated upstream
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3 (h,0, v),ForceMode.Impulse);
=======
        Move();
        CameraRotation();
        CharacterRotation();
    }
>>>>>>> Stashed changes

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

                // 물체와 플레이어 사이의 거리 측정
                float distance = Vector3.Distance(transform.position, scanObject.transform.position);

                if (distance < ObjectDistance)
                {
                    // 문열기
                    if (scanObject.CompareTag("Door"))
                    {

                        anim.SetBool("isOpen", true);

                    }
                    // 물체 설명창 띄우기
                    else if (scanObject.CompareTag("Object"))
                    {
                        manager.Action(scanObject);

                    }
                }


            }
            else
            {
                scanObject = null;
            }
        }
    }
}
