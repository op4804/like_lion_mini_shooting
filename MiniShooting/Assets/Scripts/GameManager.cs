using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance = null;

    public GameObject upgradeMenu;
    public GameObject arrow;
    public Transform upgradeMenuParent;

    //메뉴 표기 기능을 위한 함수
    private List<Text> optionTexts = new List<Text>();
    private int selectedOption = 0;
    private bool isMenuActive = false;
    public Text expText;

    //라이프 이미지
    public Image[] lifeImg;

    private bool isGameOver;

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

    private void Start()
    {
        CreateUpgradeOptions(); // UI 생성
        upgradeMenu.SetActive(false); // 처음에는 메뉴 숨김
    }

    private void Update()
    {
        if (isMenuActive)
        {
            HandleMenu(); // 키보드로 메뉴 조작
        }
        //임시로 레벨업UI 활성화버튼
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleMenu();
        }
    }

    //메뉴 활성화
    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        upgradeMenu.SetActive(isMenuActive);

        if (isMenuActive)
        {
            Time.timeScale = 0f; //시간 멈춤
            UpdateArrowPosition();
        }
        else
        {
            Time.timeScale = 1f; //시간 다시 흐름
        }
    }

    //메뉴 선택 키 입력
    private void HandleMenu()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedOption = (selectedOption - 1 + Upgrade.Instance.GetCount()) % Upgrade.Instance.GetCount();
            UpdateArrowPosition();
            Debug.Log(selectedOption);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedOption = (selectedOption + 1) % Upgrade.Instance.GetCount();
            UpdateArrowPosition();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Upgrade.Instance.UpgradeStack(selectedOption);
            ToggleMenu();
        }

        
    }

    //메뉴 생성 및 정렬 (텍스트만 생기는 상태)
    private void CreateUpgradeOptions()
    {
        RectTransform canvasRect = upgradeMenu.GetComponent<RectTransform>();
        float screenHeight = canvasRect.rect.height;
        float menuHeight = Upgrade.Instance.GetCount() * 100f;
        float startY = screenHeight / 2 - (menuHeight / 2);

        for (int i = 0; i < Upgrade.Instance.GetCount(); i++)
        {
            GameObject textObject = new GameObject($"UpgradeOption_{i}", typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(upgradeMenuParent, false);

            Text textComponent = textObject.GetComponent<Text>();
            textComponent.text = Upgrade.Instance.GetUpgradeString(i);
            textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            textComponent.fontSize = 24;
            textComponent.color = Color.white;
            textComponent.alignment = TextAnchor.MiddleLeft;

            RectTransform textTransform = textObject.GetComponent<RectTransform>();
            textTransform.sizeDelta = new Vector2(400f, 50f);

            textTransform.anchoredPosition = new Vector2(150, startY - (i * 50f));

            optionTexts.Add(textComponent);
        }
        UpdateArrowPosition();
    }

    //메뉴 선택 화살표 위치 선정
    private void UpdateArrowPosition()
    {
        if (optionTexts.Count > 0)
        {
            Vector2 targetPosition = optionTexts[selectedOption].transform.position;
            targetPosition.x -= 280f;
            arrow.transform.position = targetPosition;
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }

    //레벨, 경험치 표기
    public void ViewExp(float playerExp, int playerLevel)
    {
        expText.text = $"Level : {playerLevel} \nExp : {playerExp}";
    }

    public void UpdateLife(int playerLevel, int maxLifeCount, int currentLifeCount)
    {
        int viewMaxLife = maxLifeCount / 2;
        int viewCurrentLife = currentLifeCount / 2;

        if (playerLevel < 3)
        {
            if (lifeImg[0]==null)Debug.Log("0번 비활성화");
            lifeImg[0].gameObject.SetActive(true);
            RectTransform lifeTransform = lifeImg[0].GetComponent<RectTransform>();
            lifeTransform.anchoredPosition = new Vector2(0,0);
        }
    }
}
