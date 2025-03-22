using UnityEngine;
using System.Collections.Generic;
using System;

//��ų ������-> ��ų -> ��ų�Ŵ��������� ����˴ϴ�.
//�ش� Ŭ������ ��ų���� �������ִ� ��ų�Ŵ����Դϴ�.
//��ų�� ȿ���� ��, ������ ���ְ� ������ִ� ����� �մϴ�.
//å���� ���ְ� �������ִ� �缭���ִ� �������̶�� �����Ͻø� �ɰͰ����ϴ�.

//��ų �Ŵ������� ���� �����ؾ��ϴ� ������ �����ϴ�.
//��ų�� �����Ǿ����� ���̶�Ű���� ������Ʈ�� ��ũ��Ʈ�� ������ְ�
//��ų�Ŵ����� skills�� +�� ���� ����ĭ�� �߰��� �ش� ������Ʈ�� �־��ָ�˴ϴ�.

//�нú� ��ų
//��ų�� Ȱ��ȭ ���θ� �ٲ��ָ� Ȱ��ȭ�� ���� ȿ���� ���� �ٷ� ��ø ����˴ϴ�.
//���� ��ų�� ���ٸ� �⺻ �߻�ü�� �߻�˴ϴ�.

//��Ƽ�� ��ų
//�ݵ�� �ϳ��� ȿ���� ���־���մϴ�.

public class SkillManager : MonoBehaviour
{
    [HideInInspector]
    public static SkillManager Instance = null;

    public GameObject defaultBulletPrefab;
    private GameObject currentBulletPrefab;

    public List<Skill> skills = new List<Skill>(); //��ų ���
    private List<Action<GameObject>> bulletModifiers = new List<Action<GameObject>>(); //�нú� ��ų ����

    // �߻�ü�� ȿ�� ī��Ʈ ����
    private Dictionary<GameObject, HashSet<string>> bulletEffectStates = new Dictionary<GameObject, HashSet<string>>();

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

        currentBulletPrefab = defaultBulletPrefab; // �⺻ �߻�ü�� ȿ���� ����
    }

    private void Start()
    {
        foreach (var skill in skills)
        {
            skill.InitializeSkill(Player.Instance); //�÷��̾� ������ ������
            skill.ApplyEffect(); //��ų�� ȿ�� ���
        }

        Debug.Log($"���� ��ų �� : {skills.Count}");
    }

    private void Update()
    {
        if (skills.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.Z)) //zŰ�� ���� ��Ƽ�� ��ų ���
        {
            foreach (var skill in skills)
            {
                if (skill.IsActiveSkill && skill.IsUnlockSkill())
                    skill.UseSkill();
            }
        }
    }

    //�߻�ü ȿ�� ���
    public void AddBulletModifier(Action<GameObject> modifier)
    {
        bulletModifiers.Add(modifier);
    }

    // �߻�ü ���� + ȿ�� ����
    public GameObject CreateBullet(Vector3 position)
    {
        if (currentBulletPrefab == null) return null;

        GameObject bullet = ResourceManager.Instance.Create(currentBulletPrefab.name, position);

        foreach (var modifier in bulletModifiers)
        {
            modifier.Invoke(bullet); //���� �߻�ü�� ���� ȿ���� ���� �����ݴϴ�.
        }

        return bullet;
    }

    //�߻�ü ȿ�� ������ ȣ���ؾ���. �߻��ϴ� �߻�ü�� ȿ�� ���� ���¸� ����
    public void RegisterBulletEffect(GameObject bullet, string effectName)
    {
        if (!bulletEffectStates.ContainsKey(bullet))
            bulletEffectStates[bullet] = new HashSet<string>();

        bulletEffectStates[bullet].Add(effectName);

        //    Debug.Log($"{bullet}�� {effectName} ȿ�� ��ϵ�", bullet);
        //else
        //    Debug.LogWarning($"{bullet}�� {effectName} ȿ�� �ߺ� ��� �õ�", bullet);
    }

    // ȿ�� ������ destroy ��� ȣ���ؾ��� destroy�� skillManager���� ����
    // �߻�ü ������� ������ ��� ȿ���� �����ķ� �����մϴ�.
    public void NotifyEffectComplete(GameObject bullet, string effectName)
    {
        if (!bulletEffectStates.ContainsKey(bullet)) return;

        bulletEffectStates[bullet].Remove(effectName);

        if (bulletEffectStates[bullet].Count == 0)
        {
            bulletEffectStates.Remove(bullet);
            ResourceManager.Instance.Deactivate(bullet);
        }
    }

    //�߻�ü ȿ���� �ִ��� �Ǻ��ϴ� �Լ��Դϴ�.
    //���� ȿ���� �����Ѵٸ� �⺻ �߻�ü�� �浹 �� ��������ʰ� ��� ȿ���� ������ ������� �˴ϴ�.
    public bool IsBulletHaveEffect(GameObject bullet, string effectName)
    {
        return bulletEffectStates.ContainsKey(bullet) && bulletEffectStates[bullet].Contains(effectName);
    }
}
