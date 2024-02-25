using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventorySlotPrefab; // 인벤토리 슬롯 프리팹
    public List<Transform> slotTransforms; // 슬롯의 Transform 리스트
    public List<GameObject> inventorySlots = new List<GameObject>(); // 인벤토리 슬롯 리스트

    public void AddToInventory(Sprite itemImage)
    {
        // 인벤토리 슬롯이 꽉 차있는지 확인
        if (inventorySlots.Count < 10)
        {
            // 비어있는 슬롯을 찾아서 오브젝트 추가
            for (int i = 0; i < 10; i++)
            {
                if (inventorySlots[i] == null)
                {
                    GameObject slot = Instantiate(inventorySlotPrefab, slotTransforms[i]);
                    slot.GetComponent<Image>().sprite = itemImage;
                    inventorySlots[i] = slot;
                    break; // 슬롯 할당 후 반복문 종료
                }
            }
        }
        else
        {
            Debug.Log("인벤토리가 꽉 찼습니다.");
        }
    }
}