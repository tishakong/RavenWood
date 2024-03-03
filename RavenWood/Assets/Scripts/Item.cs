using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    GameObject item;
    GameObject target;

    public MakePotion makePotion;
    public PlayerMove playerMove;

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
                OpenDoorWithKey();
                break;
            case "Wine1":
            case "Wine2":
            case "Water":
            case "Medicine":
            case "Poison1":
            case "Poison2":
            case "Poison3":
            case "Flower":
            case "Tomato":
                MakePotionWithItem();
                break;
        }
    }

    void match()
    {
        if(target.name.Contains("candle") || target.name.Contains("Gas_Burner"))
        {
            foreach (Transform child in target.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    void MakePotionWithItem()
    {
        if (target.name.Contains("Pot"))
        {
            foreach (Transform child in target.transform)
            {
                if (item.name.Contains("Wine") && child.name == "Wine")
                {
                    child.gameObject.SetActive(true);
                    break;
                }
                else if (item.name.Contains("Poison") && child.name == "Poison")
                {
                    child.gameObject.SetActive(true);
                    break;
                }
                else if (item.name.Contains("Water") && child.name == "Water")
                {
                    child.gameObject.SetActive(true);
                    break;
                }
                else if (item.name.Contains("Medicine") && child.name == "Potion")
                {
                    child.gameObject.SetActive(true);
                    break;
                }
                else if (item.name.Contains("Flower") && child.name == "Flower")
                {
                    child.gameObject.SetActive(true);
                    break;
                }
                else if (item.name.Contains("Tomato") && child.name == "Tomato")
                {
                    child.gameObject.SetActive(true);
                    break;
                }
            }
            Destroy(item);
        }
    }

    void OpenDoorWithKey()
    {
        Key key = item.GetComponent<Key>();

        if (target.name.Contains("Door."))
        {
            Debug.Log(target);
            for (int i=0; i < key.doors.Length; i++)
            {
                if (target == key.doors[i])
                {
                    playerMove.DoorOpen = true;
                    Destroy(item);
                }
            }
        }
    }
}
