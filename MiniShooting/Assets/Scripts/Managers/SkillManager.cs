using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    [HideInInspector]
    public static SkillManager Instance = null;

    public List<Skill> skills = new List<Skill>();
    private int currentSkillIndex = 0;

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
        //�нú� ��ų ����
        foreach (var skill in skills)
        {
            skill.InitializeSkill(Player.Instance);

        }
        Debug.Log($"���� ��ų �� : {skills.Count}");
    }

    private void Update()
    {
        if (skills.Count == 0) return;

        //��Ƽ�� ��ų ���
        if (Input.GetKeyDown(KeyCode.Z) && skills[currentSkillIndex].IsActiveSkill)
        {
            skills[currentSkillIndex].UseSkill();
        }

        
    }
}
