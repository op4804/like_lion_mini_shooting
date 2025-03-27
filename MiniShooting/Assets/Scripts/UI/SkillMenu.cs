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
        InitializeSkillMenu();
    }

    void Update()
    {
        if (isMenuActive) HandleMenu();
    }
    //메뉴실행
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
    //값 랜덤 뽑기
    public void Random()
    {
        HashSet<int> randomSet = new HashSet<int>();
        System.Random rand = new System.Random();

        while (randomSet.Count < 3 && holdIndex.Count < skillList.Count)
        {
            int value = rand.Next(0, skillList.Count);
            if (holdIndex.Contains(value)) continue;
            randomSet.Add(value); // 이미 있으면 안 들어감
        }
        randomList = randomSet.ToList();
        SetSkillName();
        SetSkillIMG();
    }

    //스킬명 설정함수
    public void SetSkillName()
    {
        skillName1.text = SkillNum[randomList[0]];
        skillName2.text = SkillNum[randomList[1]];
        skillName3.text = SkillNum[randomList[2]];
    }
    //스킬이미지 변경함수
    public void SetSkillIMG()
    {
        skillImg1.sprite = skillImages[randomList[0]];
        skillImg2.sprite = skillImages[randomList[1]];
        skillImg3.sprite = skillImages[randomList[2]];
    }

    private void InitializeSkillMenu()
    {
        option = 0;
        SelectSkill();
    }

    public void HandleMenu()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (option != 0) option--;
            SelectSkill();
            Debug.Log($"선택된 스킬 : {skillList[randomList[option]].name} 스킬타입 : {skillList[randomList[option]].GetSkillType()}");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (option != 2) option++;
            SelectSkill();
            Debug.Log($"선택된 스킬 : {skillList[randomList[option]].name} 스킬타입 : {skillList[randomList[option]].GetSkillType()}");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            holdIndex.Add(randomList[option]);
            skillList[randomList[option]].IsUnlockSkill();
            Debug.Log($"선택된 스킬 : {skillList[randomList[option]].name} 스킬타입 : {skillList[randomList[option]].GetSkillType()}");
            skillSelection = skillList[randomList[option]];
            SetSkillUnlock();
            ToggleMenu();
        }
    }
    public void SelectSkill()
    {
        SetAlpha(skillImg1, 0.5f);
        SetAlpha(skillImg2, 0.5f);
        SetAlpha(skillImg3, 0.5f);
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
