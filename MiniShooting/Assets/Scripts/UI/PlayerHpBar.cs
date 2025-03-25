using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [HideInInspector]
    public static PlayerHpBar Instance = null;

    //�÷��̾� ����
    int viewMaxLifeCount;
    int viewCurrentLifeCount;
    int viewPlayerLevel;

    //������ �̹���
    public Image[] lifeImg;
    private List<GameObject> lifeList = new List<GameObject>();
    public Transform lifeParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void UpdateLife()
    {
        getPlayerStatus();

        foreach (GameObject life in lifeList)
        {
            Destroy(life);
        }
        lifeList.Clear();

        (Image lifeMax, Image lifeCurrent) = GetLifeImages();

        int fullHearts = viewCurrentLifeCount / 2; // ������ ü�� ������ ����
        bool hasHalfHeart = viewCurrentLifeCount % 2 == 1; // ������ ���� ü�� ����

        for (int i = 0; i < viewMaxLifeCount; i++)
        {
            GameObject setLife = Instantiate(lifeMax.gameObject, lifeParent);
            setLife.SetActive(true);
            RectTransform lifeTransform = setLife.GetComponent<RectTransform>();

            // ��ǥ ���� ���� �Ʒ��� ����
            lifeTransform.anchorMin = new Vector2(0, 0);
            lifeTransform.anchorMax = new Vector2(0, 0);
            lifeTransform.pivot = new Vector2(0, 0);

            // ������ ��ġ
            lifeTransform.anchoredPosition = new Vector2((50 * i)+10, 5);
            lifeList.Add(setLife);

            // ���� ü�� ������ �߰�
            if (i < fullHearts || (i == fullHearts && hasHalfHeart))
            {
                GameObject currentLife = Instantiate(lifeCurrent.gameObject, setLife.transform);
                currentLife.SetActive(true);

                if (i == fullHearts && hasHalfHeart)
                {
                    // ������ ü�� �������� �ݸ� ���̰� ����
                    Image img = currentLife.GetComponent<Image>();
                    if (img != null)
                    {
                        img.fillAmount = 0.4f; 
                    }
                }
            }
        }
    }

    //�÷��̾��� ������ �޾ƿ��� �Լ�
    private void getPlayerStatus()
    {
        viewMaxLifeCount = Player.Instance.GetMaxHealth() / 2;
        viewCurrentLifeCount = Player.Instance.GetCurrentHealth();
        viewPlayerLevel = Player.Instance.GetPlayerLevel();
    }

    //������ ���� �̹����� �����ϴ� �Լ�
    private (Image max, Image current) GetLifeImages()
    {
        int index = viewPlayerLevel < 4 ? 0 : viewPlayerLevel < 7 ? 2 : 4;
        return (lifeImg[index], lifeImg[index + 1]);
    }
}
