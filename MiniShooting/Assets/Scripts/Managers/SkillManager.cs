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

        currentBulletPrefab = defaultBulletPrefab; //효과 없을때 기본 발사체
    }

    private void Start()
    {
        //패시브 스킬 적용
        foreach (var skill in skills)
        {
            skill.InitializeSkill(Player.Instance);
            skill.ApplyEffect();

        }
        Debug.Log($"보유 스킬 수 : {skills.Count}");
    }

    private void Update()
    {
        if (skills.Count == 0) return;

        //액티브 스킬 사용
        if (Input.GetKeyDown(KeyCode.Z) && skills[currentSkillIndex].IsActiveSkill)
        {
            skills[currentSkillIndex].UseSkill();
        }
    }

    //발사체 효과 추가
    public void AddBulletModifier(Action<GameObject> modifier)
    {
        bulletModifiers.Add(modifier);
    }

    //발사체 효과 적용
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

    //발사체 효과 관리
    public void SetBulletPrefab(GameObject newBulletPrefab)
    {
        currentBulletPrefab = newBulletPrefab != null ? newBulletPrefab : defaultBulletPrefab;
    }

    ////효과 지우기
    //public void RemoveBulletModifier(Action<GameObject> modifier)
    //{
    //    bulletModifiers.Remove(modifier);
    //}
}
