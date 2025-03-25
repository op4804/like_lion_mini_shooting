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
    private Text playerLevelText; //�÷��̾� �ʱ� ����
    private Text expText; // �÷��̾� �ʱ� ����ġ
    private Text maxHealthText; // �÷��̾� �ִ� �����
    private Text currentHealthText; // �÷��̾� ���� �����
    private Text fireRateText; //�÷��̾� ���� �ӵ�
    private Text attackText; //�÷��̾� ���ݷ�
    private Text playerSpeedText; //�÷��̾� �̵��ӵ�
    private Text skillsText; //�÷��̾� ���� ��ų

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
        CreateStatusText(ref nameText, "�̸�", 0);
        CreateStatusText(ref playerLevelText, "����", 1);
        CreateStatusText(ref expText, "����ġ", 2);
        CreateStatusText(ref maxHealthText, "�ִ� ü��", 3);
        CreateStatusText(ref currentHealthText, "���� ü��", 4);
        CreateStatusText(ref attackText, "���ݷ�", 5);
        CreateStatusText(ref fireRateText, "���� �ӵ�", 6);
        CreateStatusText(ref playerSpeedText, "�̵� �ӵ�", 7);
        CreateStatusText(ref skillsText, "��ų", 8);

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
        nameText.text = "- ���Ʈ�� - ";
        playerLevelText.text = $"���� : {Player.Instance.GetPlayerLevel()}";
        expText.text = $"����ġ : {Player.Instance.GetExp()}";
        maxHealthText.text = $"�ִ� ü�� : {Player.Instance.GetMaxHealth()}";
        currentHealthText.text = $"���� ü�� : {Player.Instance.GetCurrentHealth()}";
        attackText.text = $"���ݷ� : {Player.Instance.GetAttack()}";
        fireRateText.text = $"���� �ӵ� : {Player.Instance.GetFireRate()}";
        playerSpeedText.text = $"�̵� �ӵ� : {Player.Instance.GetplayerSpeed()}";
        skillsText.text = "���� ��ų : ";
    }
}
