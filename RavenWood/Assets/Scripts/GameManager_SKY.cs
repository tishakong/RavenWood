using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public ClickManager clickManager;
    public ZoomInOut zoom;
    public Camera mainCamera;

    public PlayerMove playerMove;
    public GameObject talkPanel;        // 인포창 UI
    public GameObject hintPanel;        // 힌트창 UI
    public GameObject startPanel;       // 시작 패널
    public TextMeshProUGUI startText;   // 시작 안내 문구
    private int startTextNum = 0;       // 시작 안내 문구 넘버링
    public Text timerText;              // 제한 시간 확인 텍스트
    private float timeRemaining;        // 잔여 시간 관리 변수
    public TextMeshProUGUI talkText;    // 게임창에 뜨는 텍스트
    public TextMeshProUGUI hintText;    // 힌트창에 뜨는 텍스트
    public GameObject scanObject;       // 플레이어가 조사한 대상
    public bool isAction;               // 상태 저장용 변수

    private float panelHideDelay = 3f;

    private void Awake()
    {
        timeRemaining = 1200.0f;
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
        Talk(objData.id, objData.isClue);

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

    public void DelayedHidePanel()
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

    public void OutOfHands()
    {
        talkText.text = "손이 부족하다";
        talkPanel.SetActive(true);
        Invoke("DelayedHidePanel", panelHideDelay);
    }

    public void NeedFire()
    {
        talkText.text = "불을 먼저 붙여야 할 것 같다.";
        talkPanel.SetActive(true);
        Invoke("DelayedHidePanel", panelHideDelay);
    }

    public void NeedIngredient()
    {
        talkText.text = "재료를 넣고 섞어야할 것 같다.";
        talkPanel.SetActive(true);
        Invoke("DelayedHidePanel", panelHideDelay);
    }

    public void TurnOffStartPanel()
    {
        startTextNum++;
        if (startTextNum == 1)
        {
            mainCamera.AddComponent<SmoothCameraRotation>();
            startText.text = "여긴 어디지? 분명 레이븐우드 영주와 만찬을 즐기고 있었는데..";
        }
        if (startTextNum == 2)
        {
            startText.text = "영주가 발광석 광산 계약을 얘기하며 건네준 와인을 마신 것이 마지막 기억이다.";
        }
        else if (startTextNum == 3)
        {
            startText.text = "...";
        }
        else if (startTextNum == 4)
        {
            startText.text = "레이븐우드 영주가 독에 해박하다는 소문이 있었는데... 설마?";
        }
        else if (startTextNum == 5)
        {
            startText.text = "다시 돌아보니 의심스러운 정황이 많다. 분명 독을 사용했다. \n그렇지 않으면 내가 갑자기 정신을 잃을리 없지.";
        }
        else if (startTextNum == 6)
        {
            startText.text = "이럴 때가 아니다. 인기척이 없는 것을 보니 지금이 기회다. \n독이 퍼지고 영주가 돌아오기 전에 여기를 탈출해야만 한다.";
        }
        else if (startTextNum == 7)
        {
            SmoothCameraRotation smoothCameraRotation = mainCamera.GetComponent<SmoothCameraRotation>();
            if (smoothCameraRotation != null)
            {
                smoothCameraRotation.isStart=true;
            }
            else
            {
                Debug.LogWarning("SmoothCameraRotation script not found on mainCamera!"); // 스크립트를 찾지 못한 경우 경고를 출력합니다.
            }
            startPanel.SetActive(false);
        }
    }
}
