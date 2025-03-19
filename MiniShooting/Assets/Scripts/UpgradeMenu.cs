using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [HideInInspector]
    public static UpgradeMenu Instance = null;

    public GameObject arrow;
    public GameObject upgradePanel;

    private List<GameObject> optionTexts = new List<GameObject>();
    private int selectedOption = 0;
    private bool isMenuActive = false;
    private int UpgradeChooseNum = 4;
    private int[] t = new int[4];

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
        CreateUpgradeOptions();
        upgradePanel.SetActive(false);
    }

    private void Update()
    {
        if (isMenuActive)
        {
            HandleMenu();
        }
    }

    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        upgradePanel.SetActive(isMenuActive);
        UpgradeRandom();
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

    private void HandleMenu()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedOption = (selectedOption - 1 + Upgrade.Instance.GetCount()) % Upgrade.Instance.GetCount();
            UpdateArrowPosition();
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

    private void CreateUpgradeOptions()
    {
        int optionCount = Upgrade.Instance.GetCount();
        float totalHeight = optionCount * 60f;
        float startY = totalHeight / 2 - 30f; // 중앙 기준 정렬
        
        for (int i = 0; i < UpgradeChooseNum; i++)
        {
            GameObject textObject = new GameObject($"UpgradeOption_{i}", typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(upgradePanel.transform, false);

            Text textComponent = textObject.GetComponent<Text>();
            textComponent.text = Upgrade.Instance.GetUpgradeString(t[i]);
            textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            textComponent.fontSize = 30;
            textComponent.color = Color.white;
            textComponent.alignment = TextAnchor.MiddleLeft;

            RectTransform textTransform = textObject.GetComponent<RectTransform>();
            textTransform.sizeDelta = new Vector2(600f, 50f);
            textTransform.anchorMin = new Vector2(0.5f, 0.5f);
            textTransform.anchorMax = new Vector2(0.5f, 0.5f);
            textTransform.pivot = new Vector2(0.5f, 0.5f);
            textTransform.anchoredPosition = new Vector2(220, startY - (i * (totalHeight / optionCount)));

            optionTexts.Add(textObject);
        }

        UpdateArrowPosition();
    }

    private void UpdateArrowPosition()
    {
        if (optionTexts.Count > 0 && arrow != null)
        {
            RectTransform textTransform = optionTexts[selectedOption].GetComponent<RectTransform>();
            Vector2 targetPosition = textTransform.anchoredPosition;
            targetPosition.x -= 370f;
            arrow.GetComponent<RectTransform>().anchoredPosition = targetPosition;
        }
    }

    private void UpgradeRandom()
    {
        //업그레이드 옵션중 랜덤으로 N개를 골라서 보여줌
        int optionCount = Upgrade.Instance.GetCount();
        
        for (int i = 0; i < UpgradeChooseNum; i++)
        {
            t[i] = Random.Range(0, optionCount);
        }
    }
}
