using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MakePotion : MonoBehaviour
{
    public GameManager gameManager;

    public float rotateSpeed;
    public Renderer resultRenderer;

    private bool isBoiling = false;
    public bool isBurning = false;
    private bool isWine = false;
    private bool isPoison = false;
    public bool isPerfect = false;

    List<string> list = new List<string>(); // 리스트 초기화

    void Update()
    {
        if (isBoiling)
        {
            Boil();
        }
    }

    void Boil()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    public void StartBoiling()
    {
        foreach (Transform sibling in transform.parent.transform.parent)
        {
            if (sibling.gameObject.activeSelf && sibling != transform.parent)
            {
                isBurning = true;
                break;
            }
        }

        if (isBurning)
        {
            foreach (Transform IngredientTransform in transform.parent)
            {
                if (IngredientTransform != transform)
                {
                    if (IngredientTransform.gameObject.activeSelf)
                    {
                        list.Add(IngredientTransform.name);
                    }
                    Destroy(IngredientTransform.gameObject);
                }
            }

            if(list.Count > 0)
            {
                this.gameObject.SetActive(true);
                StartCoroutine(BoilForSeconds(3f));
            }
            else
            {
                gameManager.NeedIngredient();
            }
        }
        else
        {
            gameManager.NeedFire();
        }
    }

    IEnumerator BoilForSeconds(float seconds)
    {
        isBoiling = true;
        yield return new WaitForSeconds(seconds);
        isBoiling = false;

        if (list.Count > 0)
        {
            if (list.Contains("Wine"))
            {
                isWine = true;
            }
            else if (list.Contains("Poison"))
            {
                isPoison = true;
            }
            else if (list.Intersect(new List<string> { "Water", "Potion", "Flower", "Tomato", "Milk" }).Count() == 5)
            {
                isPerfect = true;
            }

            ChangeColor(isWine, isPoison, isPerfect);
        }
    }

    public void ChangeColor(bool wine, bool poison, bool perfect)
    {
        if (resultRenderer != null)
        {
            if (wine)
            {
                resultRenderer.material.color = Color.green;
            }
            else if (poison)
            {
                resultRenderer.material.color = Color.black;
            }
            else if (perfect)
            {
                resultRenderer.material.color = Color.red;
            }
            else
            {
                resultRenderer.material.color = Color.white;
            }
        }
    }
}
