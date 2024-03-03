using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePotion : MonoBehaviour
{
    public float rotateSpeed;

    public Renderer resultRenderer;

    // 0: 꽃, 1: 우유, 2: 물, 3: 포션, 4: 토마토, 5: 결과물, 6: 와인, 7: 독
    public List<GameObject> ingredients = new List<GameObject>();

    private void Update()
    {
        Boil();
    }

    void Boil()
    {
        ingredients[5].transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    public void ChangeColor(bool wine, bool poison)
    {
        if (resultRenderer != null)
        {
            if (wine && !poison)
            {
                resultRenderer.material.color = Color.green;
            }
            else if (wine && poison || !wine && poison)
            {
                resultRenderer.material.color = Color.black;
            }
            else
            {
                resultRenderer.material.color = Color.red;
            }
        }
    }
}
