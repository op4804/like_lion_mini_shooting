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
    public GameObject option;
    public GameObject arrow;
    public Transform upgradeMenuParent;

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
        upgradeOptions.Add("Increase Speed");
        upgradeOptions.Add("Increase Fire Rate");
        upgradeOptions.Add("Increase Max Health");

        CreateUpgradeOptions(); // UI ����
        upgradeMenu.SetActive(false); // ó������ �޴� ����
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC Ű�� �޴� ����
        {
            ToggleMenu();
        }

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
            Time.timeScale = 0f;
            UpdateArrowPosition();
        }
        else
        {
            Time.timeScale = 1f;
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
    // TODO: ���� ������ �߾ȵǳ׿�. �����ؾߵɰ� �����ϴ�.
    private void CreateUpgradeOptions()
    {
        RectTransform canvasRect = upgradeMenu.GetComponent<RectTransform>();
        float screenHeight = canvasRect.rect.height;
        float menuHeight = upgradeOptions.Count * 100f;
        float startY = screenHeight/2 - (menuHeight / 2); 

        for (int i = 0; i < upgradeOptions.Count; i++)
        {
            GameObject newOption = Instantiate(option, upgradeMenuParent);
            Text textComponent = newOption.GetComponent<Text>();
            textComponent.text = upgradeOptions[i];
            optionTexts.Add(textComponent);

            Vector2 position = newOption.transform.position;
            position.y = startY - (i * 100f);
            position.x = canvasRect.rect.width / 2;
            newOption.transform.position = position;
        }
        UpdateArrowPosition();
    }

    //�޴� ���� ȭ��ǥ ��ġ ����
    private void UpdateArrowPosition()
    {
        if (optionTexts.Count > 0)
        {
            Vector2 targetPosition = optionTexts[selectedOption].transform.position;
            targetPosition.x -= 450;
            targetPosition.y += 20;
            arrow.transform.position = targetPosition;
        }
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
    }

    public void ViewExp(float playerExp, int playerLevel)
    {
        expText.text = $"Level : {playerLevel} \nExp : {playerExp}";
    }
}
