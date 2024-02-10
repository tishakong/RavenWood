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
        // 100번대: 가구
        talkData.Add(100, "수상한 잡동사니가 모여있다.");
        talkData.Add(200, "알 수 없는 책이 잔뜩 꽂혀있다.");
        talkData.Add(300, "여기에서 단서를 찾긴 어려울 것 같다.");
        talkData.Add(400, "굳게 잠겨 열리지 않는다.");
        talkData.Add(500, "선반 위에 많은 것이 올라가 있지 않다.");
        talkData.Add(600, "누군가 사용했던 흔적이 있는 탁자이다.");
        talkData.Add(700, "영주가 사용하는 책상이다.");
        talkData.Add(800, "푹신해 보이는 의자이다.");
        talkData.Add(900, "낮은 협탁이다.");

        // 10번대: 잡동사니
        talkData.Add(10, "처리하다 만 서류뭉치이다.");
        talkData.Add(20, "구식 가스버너이다.");
        talkData.Add(30, "사용감이 느껴지는 깃펜이다.");
        talkData.Add(40, "약재를 빻는데 사용되었을 것이다.");
        talkData.Add(50, "생각보다 시간이 많이 남지 않았다.");
    }

    public string GetTalk(int id)
    {

        return talkData[id];

    }
}
