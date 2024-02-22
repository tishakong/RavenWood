using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ClickManager clickManager;
    public GameObject talkPanel;        // 게임 인포창
    public GameObject Inventory;        // 인벤토리 UI
    public TextMeshProUGUI talkText;    // 게임창에 뜨는 텍스트
    public GameObject scanObject;       // 플레이어가 조사한 대상
    public bool isAction;               // 상태 저장용 변수
    public bool isInventoryActivate;    // 인벤토리 활성화 여부 확인 변수

    private float panelHideDelay = 5f;

    private void Awake()
    {
        Inventory.SetActive(false);
        isInventoryActivate = false;
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id);

        talkPanel.SetActive(isAction);

        if (isAction)
        {
            Invoke("DelayedHidePanel", panelHideDelay);
        }
    }

    public void ShowInventory()
    {
        Inventory.SetActive(true);
        isInventoryActivate = true;
    }

    void Talk(int id)
    {
        int talkIndex = 0;
        string talkData = clickManager.GetTalk(id);

        if (talkIndex > 0)
        {
            isAction = false;
            return;
        }

        talkText.text = talkData;
        isAction = true;
        talkIndex++;
    }

    void DelayedHidePanel()
    {
        talkPanel.SetActive(false);
        isAction = false;
    }
}
