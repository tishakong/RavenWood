using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    Dictionary<int, string> talkData;  // int: 오브젝트 id, string[]: 설명

    private void Awake()
    {
        talkData = new Dictionary<int, string>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(100, "알 수 없는 책이 잔뜩 꽂혀있다.");
    }

    public string GetTalk(int id)
    {

        return talkData[id];

    }
}
