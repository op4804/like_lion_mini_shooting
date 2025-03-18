using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [HideInInspector]
    public static PlayerHpBar Instance = null;

    //플레이어 정보
    int viewMaxLifeCount;
    int viewCurrentLifeCount;
    int viewPlayerLevel;

    //라이프 이미지
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

        int fullHearts = viewCurrentLifeCount / 2; // 완전한 체력 아이콘 개수
        bool hasHalfHeart = viewCurrentLifeCount % 2 == 1; // 마지막 반쪽 체력 여부

        for (int i = 0; i < viewMaxLifeCount; i++)
        {
            GameObject setLife = Instantiate(lifeMax.gameObject, lifeParent);
            setLife.SetActive(true);
            RectTransform lifeTransform = setLife.GetComponent<RectTransform>();

            // 좌표 기준 왼쪽 아래로 변경
            lifeTransform.anchorMin = new Vector2(0, 0);
            lifeTransform.anchorMax = new Vector2(0, 0);
            lifeTransform.pivot = new Vector2(0, 0);

            // 라이프 배치
            lifeTransform.anchoredPosition = new Vector2((50 * i)+10, 5);
            lifeList.Add(setLife);

            // 현재 체력 아이콘 추가
            if (i < fullHearts || (i == fullHearts && hasHalfHeart))
            {
                GameObject currentLife = Instantiate(lifeCurrent.gameObject, setLife.transform);
                currentLife.SetActive(true);

                if (i == fullHearts && hasHalfHeart)
                {
                    // 마지막 체력 아이콘을 반만 보이게 설정
                    Image img = currentLife.GetComponent<Image>();
                    if (img != null)
                    {
                        img.fillAmount = 0.4f; 
                    }
                }
            }
        }
    }

    //플레이어의 정보를 받아오는 함수
    private void getPlayerStatus()
    {
        viewMaxLifeCount = Player.Instance.GetMaxHealth() / 2;
        viewCurrentLifeCount = Player.Instance.GetCurrentHealth();
        viewPlayerLevel = Player.Instance.GetPlayerLevel();
    }

    //레벨에 따라 이미지를 선택하는 함수
    private (Image max, Image current) GetLifeImages()
    {
        int index = viewPlayerLevel < 4 ? 0 : viewPlayerLevel < 7 ? 2 : 4;
        return (lifeImg[index], lifeImg[index + 1]);
    }
}
