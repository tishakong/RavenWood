using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Color highlightColor; // 선택된 이미지 색상
    public PlayerMove playerMove;

    void Start()
    {
        highlightColor = new Color32(255, 156, 0, 255);
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Selected);
        playerMove = GetComponent<PlayerMove>();
    }


    private void Selected()
    {
        // 현재 이미지의 한 단계 부모 GameObject 가져오기
        Transform parentTransform = transform.parent;

        Image parentImage = parentTransform.GetComponent<Image>();
        parentImage.color = highlightColor;
        playerMove.selectedItem= this.gameObject;
    }
}
