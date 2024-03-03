using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    private float initialBlinkInterval = 1.0f; // 초기 깜빡이는 간격 설정
    private Image image;
    public GameObject StartTextPanel;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        float blinkInterval = initialBlinkInterval;

        while (blinkInterval > 0.1f)
        {
            // 페이드아웃
            while (image.color.a > 0)
            {
                Color color = image.color;
                color.a -= Time.deltaTime / blinkInterval;
                image.color = color;
                yield return null;
            }

            // 알파 값이 0보다 작을 경우를 방지하기 위해 0으로 설정
            Color zeroAlphaColor = image.color;
            zeroAlphaColor.a = 0;
            image.color = zeroAlphaColor;

            yield return new WaitForSeconds(blinkInterval);

            // 페이드인
            while (image.color.a < 1)
            {
                Color color = image.color;
                color.a += Time.deltaTime / blinkInterval;
                image.color = color;
                yield return null;
            }

            // 알파 값이 1보다 클 경우를 방지하기 위해 1로 설정
            Color fullAlphaColor = image.color;
            fullAlphaColor.a = 1;
            image.color = fullAlphaColor;

            // blinkInterval을 감소시킵니다.
            blinkInterval -= 0.3f;
        }
        StartTextPanel.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
