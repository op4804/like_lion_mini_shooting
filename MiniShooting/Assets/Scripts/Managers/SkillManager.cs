using UnityEngine;
using System.Collections.Generic;
using System;

public class SkillManager : MonoBehaviour
{
    [HideInInspector]
    public static SkillManager Instance = null;

    public GameObject defaultBulletPrefab;
    private GameObject currentBulletPrefab;
    public List<Skill> skills = new List<Skill>();
    private List<Action<GameObject>> bulletModifiers = new List<Action<GameObject>>();

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

        currentBulletPrefab = defaultBulletPrefab; //ȿ�� ������ �⺻ �߻�ü
    }

    private void Start()
    {
        //�нú� ��ų ����
        foreach (var skill in skills)
        {
            skill.InitializeSkill(Player.Instance);
            skill.ApplyEffect();

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

    //�߻�ü ȿ�� �߰�
    public void AddBulletModifier(Action<GameObject> modifier)
    {
        bulletModifiers.Add(modifier);
    }

    //�߻�ü ȿ�� ����
    public GameObject CreateBullet(Vector3 position)
    {
        if (currentBulletPrefab == null)
        {
            return null;
        }

        GameObject bullet = Instantiate(currentBulletPrefab, position, Quaternion.identity);

        foreach (var modifier in bulletModifiers)
        {
            modifier.Invoke(bullet);
        }

        return bullet;
    }

    //�߻�ü ȿ�� ����
    public void SetBulletPrefab(GameObject newBulletPrefab)
    {
        currentBulletPrefab = newBulletPrefab != null ? newBulletPrefab : defaultBulletPrefab;
    }

    ////ȿ�� �����
    //public void RemoveBulletModifier(Action<GameObject> modifier)
    //{
    //    bulletModifiers.Remove(modifier);
    //}
}
