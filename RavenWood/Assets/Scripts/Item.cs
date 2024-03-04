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

    public PlayerMove playerMove;

    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
    }

    public void UseItem(GameObject selecteditem, GameObject scanObject)
    {
        item= selecteditem;
        target = scanObject;

        switch (item.name)
        {
            case "match":
                match();
                break;
            case string itemName when itemName.Contains("Key"):
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
        print("Key를 사용함");
        if (target.name.Contains("Door"))
        {
            print("상호작용한 오브젝트가 Door임");
            string TargetKey = item.name.Replace("Key", "");
            string TargetDoorParent = target.transform.parent.name.Replace("Door", "");
            string TargetDoor = target.name.Replace("Door", "");
            if (TargetKey==TargetDoor || TargetKey==TargetDoorParent)
            {
                print("키와 도어명이 일치함");
                playerMove.DoorEvent();
                Destroy(item);
                Destroy(target.GetComponent<ObjectData>());
                Destroy(target.GetComponentInParent<ObjectData>());
            }
        }
    }
}
