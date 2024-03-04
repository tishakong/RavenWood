using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class GameManager : MonoBehaviour
{
    public ClickManager clickManager;
    public ZoomInOut zoom;

    public PlayerMove playerMove;
    public AudioManager audioManager;
    public MakePotion makePotion;
    public Camera mainCamera;

    public GameObject talkPanel;        // 인포창 UI
    public GameObject hintPanel;        // 힌트창 UI
    public GameObject startPanel;       // 시작 패널
    public GameObject endPanel;
    public GameObject gameOver;         // 게임 오버 패널
    public GameObject gameClear;

    public Text timerText;              // 제한 시간 확인 텍스트
    private float timeRemaining;        // 잔여 시간 관리 변수
    bool isTimerRunning = true;         // 타이머 진행 여부
    public bool isRecovery = false;

    public TextMeshProUGUI startText;   // 시작 안내 문구
    public TextMeshProUGUI talkText;    // 게임창에 뜨는 텍스트
    public TextMeshProUGUI hintText;    // 힌트창에 뜨는 텍스트
    public TextMeshProUGUI endText;     // 포션 엔딩창 문구

    public GameObject scanObject;       // 플레이어가 조사한 대상
    public bool isAction;               // 상태 저장용 변수
    bool isStartPanel = false;
    int startTextNum;

    private float panelHideDelay = 3f;
    private Coroutine displayTextCoroutine;

    private void Awake()
    {
        timeRemaining = 1200.0f;
    }

    void Update()
    {
        if (isTimerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timeString;
        }
        else if (timeRemaining <= 0)
        {
            GameOver();
        }
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        if (objData != null)
        {
            Talk(objData.id, objData.isClue);
        }
        

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
            audioManager.PlaySound("UIBtnClk");
            isAction = false;
            hintPanel.SetActive(isAction);
            playerMove.EnableRotation();
        }
    }

    public void OutOfHands()
    {
        talkText.text = "손이 부족하다. 가방을 먼저 얻어야겠다.";
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

    public void StopTimer()
    {
        isTimerRunning = false;
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeString;
    }

    public void PotionEnding()
    {
        audioManager.PlaySound("Drinking");
        if (makePotion.isPerfect)
        {
            isRecovery = true;
            StopTimer();
            timerText.color = Color.green;
            startTextNum = 10;
            isStartPanel = true;

            // Call the coroutine for the right potion ending
            displayTextCoroutine = StartCoroutine(DisplayTextCoroutine(RightPosionEndingPanel));
        }
        else
        {
            startTextNum = 20;
            isStartPanel = true;

            // Call the coroutine for the wrong potion ending
            displayTextCoroutine = StartCoroutine(DisplayTextCoroutine(WrongPotionEndingPanel));
        }
    }

    public void GameOver()
    {
        StopTimer();
        timerText.color = Color.red;
        gameOver.SetActive(true);
    }

    public void GameClear()
    {
        StopTimer();
        timerText.color = Color.green;
        gameClear.SetActive(true);
    }

    // Modified coroutine to display text one by one
    private IEnumerator DisplayTextCoroutine(Action displayMethod)
    {
        while (isStartPanel)
        {
            displayMethod.Invoke();
            yield return new WaitForSeconds(3f); // Adjust the delay as needed
        }
    }

    public void WrongPotionEndingPanel()
    {
        endPanel.SetActive(true);
        startTextNum++;

        if (startTextNum == 21)
        {
            endText.text = "(꿀꺽 꿀꺽)";
        }
        else if (startTextNum == 22)
        {
            endText.text = "...";
        }
        else if (startTextNum == 23)
        {
            endText.text = "이럴 수가...";
        }
        else if (startTextNum == 24)
        {
            endText.text = "설마 잘못된 약물을 넣은 것인가...";
        }
        else if (startTextNum == 25)
        {
            audioManager.PlaySound("Dead");
            endText.text = "(털썩)";
        }
        else if (startTextNum == 26)
        {
            endPanel.SetActive(false);
            isStartPanel = false;
            GameOver();
        }
    }

    public void RightPosionEndingPanel()
    {
        endPanel.SetActive(true);
        startTextNum++;

        if (startTextNum == 11)
        {
            endText.text = "(꿀꺽 꿀꺽)";
        }
        if (startTextNum == 12)
        {
            endText.text = "...";
        }
        else if (startTextNum == 13)
        {
            endText.text = "이럴 수가...";
        }
        else if (startTextNum == 14)
        {
            endText.text = "몸이 훨씬 가벼워졌어.";
        }
        else if (startTextNum == 15)
        {
            endText.text = "독이 해독된건가!";
        }
        else if (startTextNum == 16)
        {
            endText.text = "이제 이 방을 나갈 방법을 찾아야겠군.";
        }
        else if (startTextNum == 17)
        {
            endPanel.SetActive(false);
            isStartPanel = false;
        }
    }

    public void TurnOffStartPanel()
    {
        audioManager.PlaySound("UIBtnClk");
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
            GameObject playerObject = GameObject.Find("Player");
            PlayerMove playerMoveScript = playerObject.GetComponent<PlayerMove>();

            if (playerMoveScript != null)
            {
                playerMoveScript.enabled = true;
            }

            if (playerObject != null)
            {
                Destroy(mainCamera.GetComponent<SmoothCameraRotation>());
                mainCamera.transform.parent = playerObject.transform;
                mainCamera.transform.localPosition = new Vector3(0f, 0.6f, 0f);
                mainCamera.transform.localRotation = Quaternion.identity;
            }

            startPanel.SetActive(false);
        }
    }
}
