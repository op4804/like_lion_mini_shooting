using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject arrow;
    public GameObject upgradePanel;

    public bool isMenuActive = false;

    private List<GameObject> optionTexts = new List<GameObject>();
    private int selectedOption = 0;
    const int UpgradeChooseNum = 4;
    //업그레이드마다 인덱스가 있고, 랜덤으로 뽑은 인덱스들을 저장.
    int[] choosenUpgrades = new int[UpgradeChooseNum];

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

        if (isMenuActive)
        {
            Time.timeScale = 0f;
            UpdateArrowPosition();
            UIManager.Instance.IsToggle = "UpgradeMenu";
        }
        else
        {
            Time.timeScale = 1f;
            UIManager.Instance.IsToggle = null;
        }

        UpgradeRandom();
    }

    private void HandleMenu()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (selectedOption == 0) selectedOption = UpgradeChooseNum;
            selectedOption = (selectedOption - 1) % UpgradeChooseNum;
            UpdateArrowPosition();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedOption = (selectedOption + 1) % UpgradeChooseNum;
            UpdateArrowPosition();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //랜덤으로 뽑은 4개의 인덱스중 선택
            Upgrade.Instance.UpgradeStack(choosenUpgrades[selectedOption]);
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
            textComponent.text = Upgrade.Instance.GetUpgradeString(choosenUpgrades[i]);
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
        //중복확인용 자료구조
        HashSet<int> selectedIndexes = new HashSet<int>();

        while (selectedIndexes.Count < UpgradeChooseNum)
        {
            int randomValue = Random.Range(0, optionCount);

            selectedIndexes.Add(randomValue);
        }
        //choosenUpgrades에 자료 복사
        selectedIndexes.CopyTo(choosenUpgrades);

        TextUpdate(choosenUpgrades);
    }

    //실제로 표시될 텍스트 업데이트
    private void TextUpdate(int[] choosenUpgrades)
    {
        for (int i = 0; i < UpgradeChooseNum; i++)
        {
            if (i < optionTexts.Count) // 배열 범위를 벗어나지 않도록 체크
            {
                Text textComponent = optionTexts[i].GetComponent<Text>();
                textComponent.text = Upgrade.Instance.GetUpgradeString(choosenUpgrades[i]);
            }
        }
    }
}
