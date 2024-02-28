using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Color highlightColor; // 선택된 이미지 색상
    public PlayerMove playerMove;
    Image parentImage;

    void Start()
    {
        parentImage = transform.parent.GetComponent<Image>();
        highlightColor = new Color32(255, 156, 0, 255);

        Button button = GetComponent<Button>();
        button.onClick.AddListener(Selected);
        playerMove = FindObjectOfType<PlayerMove>();
    }

    private void Selected()
    {
        if (playerMove.selectedItem != null)
        {
            Image itemImage = playerMove.selectedItem.transform.parent.GetComponent<Image>();
            itemImage.color = Color.white;
        }
        playerMove.selectedItem= this.gameObject;
        parentImage.color = highlightColor;
    }
}
