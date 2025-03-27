using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;

public class SkillMenu : MonoBehaviour
{
    public Sprite[] skillImages;
    public Image skillImg1;
    public Image skillImg2;
    public Image skillImg3;
    public TextMeshProUGUI skillName1;
    public TextMeshProUGUI skillName2;
    public TextMeshProUGUI skillName3;
    public GameObject skillpanel;
    private bool isMenuActive = false;

    private Skill skillSelection;

    private List<Skill> skillList;

    int option = 1;

    Dictionary<int, string> SkillNum = new Dictionary<int, string>();
    void LoadSkills()
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i].IsUnlockSkill() == false)
                SkillNum.Add(i, skillList[i].name);
        }
    }

    void Start()
    {
        skillList = SkillManager.Instance.skills;
        skillpanel.SetActive(isMenuActive);
        LoadSkills();
        Random();
    }

    void Update()
    {
        if (isMenuActive) HandleMenu();
    }
    //�޴�����
    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;

        skillpanel.SetActive(isMenuActive);

        if (isMenuActive)
        {
            Time.timeScale = 0f;
            UIManager.Instance.IsToggle = "SkillMenu";
        }
        else
        {
            Time.timeScale = 1f;
            UIManager.Instance.IsToggle = null;
        }
        Random();
    }

    HashSet<int> holdIndex = new HashSet<int>();
    List<int> randomList;
    //�� ���� �̱�
    public void Random()
    {
        //���� �������� ���� ��ų �ε����� ����
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < skillList.Count; i++)
        {
            if (!holdIndex.Contains(i))
                availableIndices.Add(i);
        }

        // �������� ���� ��ų�� 3�� �̸��� ��� ��� ���
        if (availableIndices.Count < 3)
        {
            Debug.LogWarning("�������� ���� ��ų�� 3�� �̸��Դϴ�. ���� " + availableIndices.Count + "���� ��� �����մϴ�.");
        }

        // System.Random�� ����Ͽ� availableIndices�� ����, �ִ� 3���� ����
        System.Random rand = new System.Random();
        randomList = availableIndices.OrderBy(x => rand.Next()).Take(3).ToList();

        SetSkillName();
        SetSkillIMG();
    }

    //��ų�� �����Լ�
    public void SetSkillName()
    {
        // ù ��° ����
        if (randomList.Count > 0 && SkillNum.ContainsKey(randomList[0]))
            skillName1.text = SkillNum[randomList[0]];
        else
            skillName1.text = "";

        // �� ��° ����
        if (randomList.Count > 1 && SkillNum.ContainsKey(randomList[1]))
            skillName2.text = SkillNum[randomList[1]];
        else
            skillName2.text = "";

        // �� ��° ����
        if (randomList.Count > 2 && SkillNum.ContainsKey(randomList[2]))
            skillName3.text = SkillNum[randomList[2]];
        else
            skillName3.text = "";
    }
    //��ų�̹��� �����Լ�
    public void SetSkillIMG()
    {
        // ù ��° ����
        if (randomList.Count > 0)
            skillImg1.sprite = skillImages[randomList[0]];
        else
            skillImg1.sprite = null;

        // �� ��° ����
        if (randomList.Count > 1)
            skillImg2.sprite = skillImages[randomList[1]];
        else
            skillImg2.sprite = null;

        // �� ��° ����
        if (randomList.Count > 2)
            skillImg3.sprite = skillImages[randomList[2]];
        else
            skillImg3.sprite = null;
    }


    public void HandleMenu()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (option != 0) option--;
            SelectSkill();
            Debug.Log($"���õ� ��ų : {skillList[randomList[option]].name} ��ųŸ�� : {skillList[randomList[option]].GetSkillType()}");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (option != 2) option++;
            SelectSkill();
            Debug.Log($"���õ� ��ų : {skillList[randomList[option]].name} ��ųŸ�� : {skillList[randomList[option]].GetSkillType()}");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //������ ���� ������ �ؽü¿� �ֱ�
            holdIndex.Add(randomList[option]);
            //������ ��ų Ȱ��ȭ
            skillList[randomList[option]].UnlockSkill();
            Debug.Log($"���õ� ��ų : {skillList[randomList[option]].name} ��ųŸ�� : {skillList[randomList[option]].GetSkillType()}");
            skillSelection = skillList[randomList[option]];
            SetSkillUnlock();
            ToggleMenu();
        }
    }
    public void SelectSkill()
    {
        SetAlpha(skillImg1, 0.1f);
        SetAlpha(skillImg2, 0.1f);
        SetAlpha(skillImg3, 0.1f);
        switch (option)
        {
            case 0:
                SetAlpha(skillImg1, 1f);
                break;
            case 1:
                SetAlpha(skillImg2, 1f);
                break;
            case 2:
                SetAlpha(skillImg3, 1f);
                break;
        }
    }
    void SetAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }

    void SetSkillUnlock()
    {
        if (skillSelection != null)
        {
            if (skillSelection.IsActiveSkill == true)
                SkillManager.Instance.SetActiveSkill(skillSelection);
            else
                SkillManager.Instance.SetPassiveSkill(skillSelection);
        }
    }
}
