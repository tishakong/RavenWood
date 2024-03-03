using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    GameObject item;
    GameObject target;

    public MakePotion makePotion;

    public void UseItem(GameObject selecteditem, GameObject scanObject)
    {
        item= selecteditem;
        target = scanObject;

        switch (item.name)
        {
            case "match":
                match();
                break;
            case "Key":
                break;
            case "Wine1":
                MakePotionWithItem();
                break;
        }
    }

    void match()
    {
        if(target.name.Contains("candle"))
        {
            foreach (Transform child in target.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    void MakePotionWithItem()
    {
        if (makePotion != null)
        {
            Debug.Log(item);
            Destroy(item);

            // Find the ingredient with the same name as the destroyed item
            string itemName = item.name;

            // Use List.Find instead of Array.Find
            GameObject matchingIngredient = makePotion.ingredients.Find(ingredient => ingredient.name == itemName);
            Debug.Log(matchingIngredient);

            if (matchingIngredient != null)
            {
                // Activate the matching ingredient's child object in the target
                Transform ingredientChild = target.transform.Find(matchingIngredient.name);

                if (ingredientChild != null)
                {
                    ingredientChild.gameObject.SetActive(true);
                }
            }
        }
    }

}
