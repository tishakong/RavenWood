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
        Sprite itemImage = GetItemImage(itemObject.name);

        // 비어있는 슬롯을 찾아서 오브젝트 추가
        for (int i = 0; i < 10; i++)
        {
            if (inventorySlots[i] == null)
            {
                GameObject slotGameObject = new GameObject("SlotImage"); // 슬롯 GameObject 생성
                slotGameObject.transform.SetParent(slotTransforms[i]); // 슬롯의 부모로 설정
                slotGameObject.transform.localPosition = Vector3.zero; // 로컬 위치를 원점으로 설정

                // 슬롯에 Image 컴포넌트 추가
                Image slotImage = slotGameObject.AddComponent<Image>();
                slotImage.sprite = itemImage; // 슬롯에 이미지 할당

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