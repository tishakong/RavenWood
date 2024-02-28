using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePotion : MonoBehaviour
{
    public float rotateSpeed;

    // 0: 꽃, 1: 우유, 2: 물, 3: 포션, 4: 토마토, 5: 결과물
    public List<GameObject> ingredients = new List<GameObject>();



    void Boil()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
