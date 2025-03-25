using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    private Image expBar;
    private Image Exp;
    public GameObject ee; // Exp Image�� �ִ� ������Ʈ
    public GameObject bar;

    private float exp;
    private float expScale;
    private bool isFilling = false; // �ߺ� ���� ����

    void Awake()
    {
        expBar = GetComponent<Image>();
        bar.SetActive(true);
        if (ee == null)
        {
            Debug.LogError("ee�� null�Դϴ�! Inspector���� GameObject�� �����ϼ���.");
        }
        else
        {
            Exp = ee.GetComponent<Image>();
            expBar = bar.GetComponent<Image>();
            if (Exp == null)
            {
                Debug.LogError("Exp Image ������Ʈ�� �����ϴ�! GameObject�� Image�� �ִ��� Ȯ���ϼ���.");
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
                //����ġ�� ���� ���� �ʾ�����, ���� 100%�� ä���
                StartCoroutine(FillChange(1f, 0.5f));
            }
            else
            {
                //�Ϲ����� ����ġ ����
                StartCoroutine(FillChange(exp / expScale, 1f));
            }
        }
    }

    IEnumerator FillChange(float targetAmount, float duration)
    {
        if (Exp == null)
        {
            Debug.LogError("Exp Image�� null�Դϴ�! Inspector ������ Ȯ���ϼ���.");
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