using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Drawing;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;        // 인벤토리 UI
    public bool isInventoryActivate;    // 인벤토리 활성화 여부 확인 변수

    public List<Transform> slotTransforms; // 슬롯의 Transform 리스트
    public List<Image> inventorySlots = new List<Image>(); // 인벤토리 슬롯 리스트
    public List<Image> itemImageList = new List<Image>(); // 아이템 이미지 리스트

    private void Awake()
    {
        Inventory.SetActive(false);
        isInventoryActivate = false;
    }


    public void ShowInventory()
    {
        Inventory.SetActive(true);
        isInventoryActivate = true;
        for (int i = 0; i < 10; i++)
        {
            inventorySlots.Add(null); // 요소를 null로 초기화
        }
    }

    public void AddToInventory(GameObject itemObject)
    {
        Image itemImage = GetItemImage(itemObject.name);

        // 비어있는 슬롯을 찾아서 오브젝트 추가
        for (int i = 0; i < 10; i++)
        {
            if (inventorySlots[i] == null)
            {
                Image slotImage = Instantiate(itemImage, slotTransforms[i]);
                inventorySlots[i] = slotImage;
                break; // 슬롯 할당 후 반복문 종료
            }
        }
    }

    private Image GetItemImage(string itemName)
    {
        foreach (Image image in itemImageList)
        {
            // 아이템 이름이 이미지 이름을 포함하고 있는지 확인
            if (image.name.ToLower().Contains(itemName.ToLower()))
            {
                return image;
            }
        }
        return null;
    }
}