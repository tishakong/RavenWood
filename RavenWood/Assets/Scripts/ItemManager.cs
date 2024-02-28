using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Color highlightColor; // 선택된 이미지 색상
    public bool isSelected;

    void Start()
    {
        highlightColor = new Color32(255, 156, 0, 255);
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Selected);
    }


    private void Selected()
    {
        // 현재 이미지의 한 단계 부모 GameObject 가져오기
        Transform parentTransform = transform.parent;

        Image parentImage = parentTransform.GetComponent<Image>();
        parentImage.color = highlightColor;
        isSelected = true;
    }

    private void Update()
    {
        if (isSelected)
        {
            //무언가 오브젝트를 클릭했는지, 다른 UI를 클릭했는지, 상호작용이 가능한 오브젝트를 클릭했는지 확인하는 코드 작성 필요
        }
    }


}
