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
        //아직 보유하지 않은 스킬 인덱스를 모음
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < skillList.Count; i++)
        {
            if (!holdIndex.Contains(i))
                availableIndices.Add(i);
        }

        // 보유하지 않은 스킬이 3개 미만인 경우 경고 출력
        if (availableIndices.Count < 3)
        {
            Debug.LogWarning("보유하지 않은 스킬이 3개 미만입니다. 현재 " + availableIndices.Count + "개만 사용 가능합니다.");
        }

        // System.Random을 사용하여 availableIndices를 섞고, 최대 3개를 선택
        System.Random rand = new System.Random();
        randomList = availableIndices.OrderBy(x => rand.Next()).Take(3).ToList();

        SetSkillName();
        SetSkillIMG();
    }

    //스킬명 설정함수
    public void SetSkillName()
    {
        // 첫 번째 슬롯
        if (randomList.Count > 0 && SkillNum.ContainsKey(randomList[0]))
            skillName1.text = SkillNum[randomList[0]];
        else
            skillName1.text = "";

        // 두 번째 슬롯
        if (randomList.Count > 1 && SkillNum.ContainsKey(randomList[1]))
            skillName2.text = SkillNum[randomList[1]];
        else
            skillName2.text = "";

        // 세 번째 슬롯
        if (randomList.Count > 2 && SkillNum.ContainsKey(randomList[2]))
            skillName3.text = SkillNum[randomList[2]];
        else
            skillName3.text = "";
    }
    //스킬이미지 변경함수
    public void SetSkillIMG()
    {
        // 첫 번째 슬롯
        if (randomList.Count > 0)
            skillImg1.sprite = skillImages[randomList[0]];
        else
            skillImg1.sprite = null;

        // 두 번째 슬롯
        if (randomList.Count > 1)
            skillImg2.sprite = skillImages[randomList[1]];
        else
            skillImg2.sprite = null;

        // 세 번째 슬롯
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
            //선택한 값을 보유함 해시셋에 넣기
            holdIndex.Add(randomList[option]);
            //선택한 스킬 활성화
            skillList[randomList[option]].UnlockSkill();
            Debug.Log($"선택된 스킬 : {skillList[randomList[option]].name} 스킬타입 : {skillList[randomList[option]].GetSkillType()}");
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
