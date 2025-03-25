using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatusMenu : MonoBehaviour
{
    public GameObject playerImg;
    public GameObject statusPanel;
    GameObject textObject;

    public bool isMenuActive = false;

    private float startX = -160f;

    private List<GameObject> statusTextObjects = new List<GameObject>();

    private Text nameText;
    private Text playerLevelText; //플레이어 초기 레벨
    private Text expText; // 플레이어 초기 경험치
    private Text maxHealthText; // 플레이어 최대 생명력
    private Text currentHealthText; // 플레이어 현재 생명력
    private Text fireRateText; //플레이어 연사 속도
    private Text attackText; //플레이어 공격력
    private Text playerSpeedText; //플레이어 이동속도
    private Text skillsText; //플레이어 보유 스킬

    void Start()
    {
        statusPanel.SetActive(false);
        ViewPlayer();
        ViewStatus();
    }

    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;

        statusPanel.SetActive(isMenuActive);

        if (isMenuActive)
        {
            Time.timeScale = 0f;
            UIManager.Instance.IsToggle = "StatusMenu";
            UpdateStatus();
        }
        else
        {
            Time.timeScale = 1f;
            UIManager.Instance.IsToggle = null;
        }
    }

    public void ViewPlayer()
    {
        playerImg.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX, 0);
    }

    public void ViewStatus()
    {
        CreateStatusText(ref nameText, "이름", 0);
        CreateStatusText(ref playerLevelText, "레벨", 1);
        CreateStatusText(ref expText, "경험치", 2);
        CreateStatusText(ref maxHealthText, "최대 체력", 3);
        CreateStatusText(ref currentHealthText, "현재 체력", 4);
        CreateStatusText(ref attackText, "공격력", 5);
        CreateStatusText(ref fireRateText, "공격 속도", 6);
        CreateStatusText(ref playerSpeedText, "이동 속도", 7);
        CreateStatusText(ref skillsText, "스킬", 8);

        UpdateStatus();
    }
    private void CreateStatusText(ref Text value, string label, int index)
    {
        GameObject textObject = new GameObject($"{label}Text", typeof(RectTransform), typeof(Text));
        textObject.transform.SetParent(statusPanel.transform, false);

        value = textObject.GetComponent<Text>();
        value.text = $"{label}: 0";
        value.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        value.fontSize = 20;
        value.color = Color.white;
        value.alignment = TextAnchor.MiddleLeft;

        RectTransform textTransform = textObject.GetComponent<RectTransform>();
        textTransform.sizeDelta = new Vector2(600f, 50f);
        textTransform.anchorMin = new Vector2(0.5f, 0.5f);
        textTransform.anchorMax = new Vector2(0.5f, 0.5f);
        textTransform.pivot = new Vector2(0.5f, 0.5f);
        textTransform.anchoredPosition = new Vector2(350, 130-(index * 32));

        statusTextObjects.Add(textObject);
    }

    private void UpdateStatus()
    {
        nameText.text = "- 드미트리 - ";
        playerLevelText.text = $"레벨 : {Player.Instance.GetPlayerLevel()}";
        expText.text = $"경험치 : {Player.Instance.GetExp()}";
        maxHealthText.text = $"최대 체력 : {Player.Instance.GetMaxHealth()}";
        currentHealthText.text = $"현재 체력 : {Player.Instance.GetCurrentHealth()}";
        attackText.text = $"공격력 : {Player.Instance.GetAttack()}";
        fireRateText.text = $"공격 속도 : {Player.Instance.GetFireRate()}";
        playerSpeedText.text = $"이동 속도 : {Player.Instance.GetplayerSpeed()}";
        skillsText.text = "보유 스킬 : ";
    }
}
