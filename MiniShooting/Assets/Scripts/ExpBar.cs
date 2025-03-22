using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    private Image expBar;
    private Image Exp;
    public GameObject ee; // Exp Image가 있는 오브젝트
    public GameObject bar;

    private float exp;
    private float expScale;
    private bool isFilling = false; // 중복 실행 방지

    void Awake()
    {
        expBar = GetComponent<Image>();
        bar.SetActive(true);
        if (ee == null)
        {
            Debug.LogError("ee가 null입니다! Inspector에서 GameObject를 연결하세요.");
        }
        else
        {
            Exp = ee.GetComponent<Image>();
            expBar = bar.GetComponent<Image>();
            if (Exp == null)
            {
                Debug.LogError("Exp Image 컴포넌트가 없습니다! GameObject에 Image가 있는지 확인하세요.");
            }
        }
    }

    void Update()
    {
        exp = Player.Instance.GetExp();
        expScale = Player.Instance.GetExpScale() - 15;

        if (Player.Instance.Getflag() && !isFilling)
        {
            if (exp >= expScale && Exp.fillAmount < 1f)
            {
                //경험치가 가득 차지 않았으면, 먼저 100%로 채우기
                StartCoroutine(FillChange(1f, 0.5f));
            }
            else
            {
                //일반적인 경험치 증가
                StartCoroutine(FillChange(exp / expScale, 1f));
            }
        }
    }

    IEnumerator FillChange(float targetAmount, float duration)
    {
        if (Exp == null)
        {
            Debug.LogError("Exp Image가 null입니다! Inspector 설정을 확인하세요.");
            yield break;
        }

        isFilling = true;
        float startAmount = Exp.fillAmount;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            Exp.fillAmount = Mathf.Lerp(startAmount, targetAmount, time / duration);
            yield return null;
        }

        Exp.fillAmount = targetAmount;
        isFilling = false;
    }
}