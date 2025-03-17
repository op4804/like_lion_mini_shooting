using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance = null;

    public GameObject upgradeMenu;
    public GameObject arrow;
    public Transform upgradeMenuParent;

    //�޴� ǥ�� ����� ���� �Լ�
    private List<String> upgradeOptions = new List<String>();
    private List<Text> optionTexts = new List<Text>();
    private int selectedOption = 0;
    private bool isMenuActive = false;
    public Text expText;

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
        //��� ���� 3���� ����
        upgradeOptions.Add("Increase Speed");
        upgradeOptions.Add("Increase Fire Rate");
        upgradeOptions.Add("Increase Max Health");

        CreateUpgradeOptions(); // UI ����
        upgradeMenu.SetActive(false); // ó������ �޴� ����
    }

    private void Update()
    {
        if (isMenuActive)
        {
            HandleMenu(); // Ű����� �޴� ����
        }
    }

    //�޴� Ȱ��ȭ
    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        upgradeMenu.SetActive(isMenuActive);

        if (isMenuActive)
        {
            Time.timeScale = 0f; //�ð� ����
            UpdateArrowPosition();
        }
        else
        {
            Time.timeScale = 1f; //�ð� �ٽ� �帧
        }
    }

    //�޴� ���� Ű �Է�
    private void HandleMenu()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedOption = (selectedOption - 1 + upgradeOptions.Count) % upgradeOptions.Count;
            UpdateArrowPosition();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedOption = (selectedOption + 1) % upgradeOptions.Count;
            UpdateArrowPosition();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ToggleMenu();
        }
    }

    //�޴� ���� �� ���� (�ؽ�Ʈ�� ����� ����)
    private void CreateUpgradeOptions()
    {
        RectTransform canvasRect = upgradeMenu.GetComponent<RectTransform>();
        float screenHeight = canvasRect.rect.height;
        float menuHeight = upgradeOptions.Count * 100f;
        float startY = screenHeight / 2 - (menuHeight / 2);

        for (int i = 0; i < upgradeOptions.Count; i++)
        {
            GameObject textObject = new GameObject($"UpgradeOption_{i}", typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(upgradeMenuParent, false);

            Text textComponent = textObject.GetComponent<Text>();
            textComponent.text = upgradeOptions[i];
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

    //�޴� ���� ȭ��ǥ ��ġ ����
    private void UpdateArrowPosition()
    {
        if (optionTexts.Count > 0)
        {
            Vector2 targetPosition = optionTexts[selectedOption].transform.position;
            targetPosition.x -= 650f;
            arrow.transform.position = targetPosition;
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }

    //����, ����ġ ǥ��
    public void ViewExp(float playerExp, int playerLevel)
    {
        expText.text = $"Level : {playerLevel} \nExp : {playerExp}";
    }
}
