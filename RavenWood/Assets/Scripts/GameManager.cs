using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ClickManager clickManager;
<<<<<<< Updated upstream
    public GameObject talkPanel;        // 게임 인포창
<<<<<<< Updated upstream
=======
    public PlayerMove playerMove;
    public GameObject talkPanel;        // 인포창 UI
    public GameObject hintPanel;        // 힌트창 UI
    public GameObject Inventory;        // 인벤토리 UI
    public Text timerText;              // 제한 시간 확인 텍스트
    private float timeRemaining;        // 잔여 시간 관리 변수
>>>>>>> Stashed changes
=======
    public GameObject hintPanel;        // 힌트창 UI
>>>>>>> Stashed changes
    public TextMeshProUGUI talkText;    // 게임창에 뜨는 텍스트
    public TextMeshProUGUI hintText;    // 힌트창에 뜨는 텍스트
    public GameObject scanObject;       // 플레이어가 조사한 대상
    public bool isAction;               // 상태 저장용 변수

    private float panelHideDelay = 5f;

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

<<<<<<< Updated upstream
    void Talk(int id)
=======
    public void ShowInventory()
    {
        Inventory.SetActive(true);
        isInventoryActivate = true;
    }

    void Talk(int id, bool isClue)
>>>>>>> Stashed changes
    {
        string talkData = clickManager.GetTalk(id);

        if (!isClue)
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
