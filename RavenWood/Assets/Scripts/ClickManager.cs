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
        //0번대: 침실
        talkData.Add(1, "방금까지 내가 누워있던 폭신한 침대이다.");
        talkData.Add(2, "호수가 그려져 있는 액자다. 왠지 빛이 새어나오는 것 같다.");
        talkData.Add(3, "값이 비싸기로 유명한 발광석이다.");
        talkData.Add(4, "약재를 상자 채로 거래한 내역이 쓰여있는 장부다.");
        talkData.Add(5, "아무렇게나 쌓여있는 상자다. 힘을 주면 들 수 있을 것 같다.");
        talkData.Add(6, "무겁지만 힘을 주면 들 수 있을 것 같다.");
        talkData.Add(7, "무언가 들어있지만 잠겨있다. 힘을 주면 들 수 있을 것 같다.");
        talkData.Add(8, "서랍이 있는 책상이다.");
        talkData.Add(9, "서랍이 굳게 잠겨있다. 열쇠로 열 수 있을 것 같다.");
        talkData.Add(0, "화한 냄새가 나는 약재로 추정된다.");


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

        // 1000번대: 힌트창 나오는 오브젝트
        talkData.Add(1000, "해독제 만드는 법\n\n준비물\n1. 젠티안 우린 물 한 병\n2. 안젤리카 한 뿌리\n3. 토마토 반 개\n4. 사슴의 진액\n5. 물 한 병\n\n" +
            "순서는 중요하지 않다. 모든 것을 한 곳에 넣고 색이 변할 때까지 충분히 끓이면 된다. 만약 제대로 만들었으면 붉은 색으로 변할 것이다. " +
            "알코올이 들어가면 초록색으로, 독이 들어가면 검은색으로, 재료를 제대로 넣지 않았다면 흰색으로 변할 것이다. " +
            "색이 이상하게 변했다면 입에 댈 생각조차 해선 안된다.");
        talkData.Add(2000, "식물 도감\n\n001. 안젤리카\n피자식물문의 식물 중 하나로, 길쭉한 이파리와 붉은 열매가 특징이다.\n" +
            "보통은 약재로 많이 사용되지만, 잘못 사용하면 독으로 쓰일 수도 있다.");
        talkData.Add(3000, "이 독은 복용자를 1시간 만에 죽음에 이르게 할 수 있다.\n" +
            "이 독의 경우에는 마시면 처음엔 정신을 잃지만, 40분이 지나면 깨어나게 된다.\n" +
            "깨어난 복용자는 아무런 느낌이 들지 않아 문제가 없다고 생각할 수 있지만, 20분이 지나면 극심한 고통 속에서 죽어가게 된다.\n" +
            "이 독의 가장 큰 장점은 일상에서 쉽게 구할 수 있는 재료로 만들 수 있다는 것이다.\n" +
            "하지만 동시에 독을 만든 재료를 베이스로 쉽게 해독제를 만들 수 있다는 특징이 있다.");
    }

    public string GetTalk(int id)
    {
        return talkData[id];
    }

}
