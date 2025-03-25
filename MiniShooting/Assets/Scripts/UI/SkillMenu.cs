using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenu : MonoBehaviour
{
    public Image[] skillImages;
    public Image skillImg1;
    public Image skillImg2;
    public Image skillImg3;
    public TextMeshProUGUI skillName1;
    public TextMeshProUGUI skillName2;
    public TextMeshProUGUI skillName3;
    public GameObject skillpanel;
    private bool isMenuActive = false;

    private List<Skill> skillList;
    void Start()
    {
        SetSkillName();
        skillpanel.SetActive(isMenuActive);
        skillList = SkillManager.Instance.skills;
    }

    void Update()
    {
        
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
    }
    //�� ���� �̱�
    public void Random()
    {
        HashSet<int> randomSet = new HashSet<int>();
        System.Random rand = new System.Random();

        while (randomSet.Count < 3)
        {
            int value = rand.Next(0, skillList.Count); // 0~9 �� �ϳ�
            randomSet.Add(value); // �̹� ������ �� ��
        }
    }

    //��ų�� �����Լ�
    public void SetSkillName()
    {
        skillName1.text = "1";
        skillName2.text = "2";
        skillName3.text = "3";
    }
    //��ų�̹��� �����Լ�
    public void SetSkillIMG()
    {
        //skillImg1.sprite = ;
        //skillImg2.sprite = ;
        //skillImg3.sprite = ;
    }
}
