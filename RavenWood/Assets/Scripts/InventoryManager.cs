using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Drawing;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;        // 인벤토리 UI
    public bool isInventoryActivate;    // 인벤토리 활성화 여부 확인 변수

    public List<Transform> slotTransforms; // 슬롯의 Transform 리스트
    public List<GameObject> inventorySlots = new List<GameObject>(); // 인벤토리 슬롯 리스트
    public List<Sprite> itemImageList = new List<Sprite>(); // 아이템 이미지 리스트

    public ItemManager itemManager; // 아이템에 추가할 스크립트

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
        AddToInventory("match");
    }

    public void AddToInventory(string itemObjectName)
    {
        Sprite itemImage = GetItemImage(itemObjectName);

        for (int i = 0; i < 10; i++)
        {
            if (inventorySlots[i] == null)
            {
                // 비어있는 슬롯에 GameObject 생성
                GameObject slotGameObject = new GameObject(itemObjectName); 
                slotGameObject.transform.SetParent(slotTransforms[i]);
                slotGameObject.transform.localPosition = Vector3.zero;

                // 오브젝트에 이미지 할당, 버튼 할당
                Image slotImage = slotGameObject.AddComponent<Image>();
                slotImage.sprite = itemImage;
                slotGameObject.AddComponent<Button>();

                // 이미지 너비와 높이 설정
                RectTransform slotRectTransform = slotGameObject.GetComponent<RectTransform>();
                slotRectTransform.sizeDelta = new Vector2(80, 80);

                //itemManager 스크립트 부여
                slotGameObject.AddComponent(itemManager.GetType());

                inventorySlots[i] = slotGameObject; // 인벤토리 슬롯에 GameObject 할당

                break; // 슬롯 할당 후 반복문 종료
            }
        }
    }


    private Sprite GetItemImage(string itemName)
    {
        foreach (Sprite image in itemImageList)
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