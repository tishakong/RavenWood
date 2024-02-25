using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ClickManager clickManager;

    public PlayerMove playerMove;
    public GameObject talkPanel;        // 인포창 UI
    public GameObject hintPanel;        // 힌트창 UI
    public GameObject Inventory;        // 인벤토리 UI
    public Text timerText;              // 제한 시간 확인 텍스트
    private float timeRemaining;        // 잔여 시간 관리 변수
    public TextMeshProUGUI talkText;    // 게임창에 뜨는 텍스트
    public TextMeshProUGUI hintText;    // 힌트창에 뜨는 텍스트
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

        // 인포창
        if (!objData.isClue)
        {
            if (isAction)
            {
                talkPanel.SetActive(isAction);
                Invoke("DelayedHidePanel", panelHideDelay);
            }
        }
        // 힌트창
        else
        {
            playerMove.DisableRotation();
            hintPanel.SetActive(isAction);
        }
    }

    public void ShowInventory()
    {
        Inventory.SetActive(true);
        isInventoryActivate = true;
    }


    void Talk(int id, bool isClue)
    {
        string talkData = clickManager.GetTalk(id);

        if (id < 1000)
        {
            talkText.text = talkData;
        }
        else
        {
            hintText.text = talkData;
        }
        isAction = true;

    }

    void DelayedHidePanel()
    {
        talkPanel.SetActive(false);
        isAction = false;
    }

    public void BackButton()
    {
        // 힌트창
        if (isAction)
        {
            isAction = false;
            hintPanel.SetActive(isAction);
            playerMove.EnableRotation();
        }
    }
}
