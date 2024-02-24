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
    public Text timerText;              // 제한 시간 확인 텍스트
    private float timeRemaining;        // 잔여 시간 관리 변수
    public TextMeshProUGUI talkText;    // 게임창에 뜨는 텍스트
    public GameObject scanObject;       // 플레이어가 조사한 대상
    public bool isAction;               // 상태 저장용 변수
    public bool isInventoryActivate;    // 인벤토리 활성화 여부 확인 변수

    private float panelHideDelay = 5f;

    private void Awake()
    {
        Inventory.SetActive(false);
        isInventoryActivate = false;
        timeRemaining = 600.0f;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timeString;
        }
        else
        {
            timerText.text = "제한 시간 초과(게임오버)";
        }
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
