using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
    GameObject scanObject;
    public GameManager manager;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3 (h,0, v),ForceMode.Impulse);

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
}
