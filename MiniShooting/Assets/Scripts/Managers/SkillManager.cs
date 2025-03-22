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
    private Dictionary<GameObject, int> bulletEffectStates = new Dictionary<GameObject, int>();

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

        GameObject bullet = Instantiate(currentBulletPrefab, position, Quaternion.identity); // TODO: ������Ʈ Ǯ�� ����

        foreach (var modifier in bulletModifiers)
        {
            modifier.Invoke(bullet); //���� �߻�ü�� ���� ȿ���� ���� �����ݴϴ�.
        }

        return bullet;
    }

    //�߻�ü ȿ�� ������ ȣ���ؾ���. �߻��ϴ� �߻�ü�� ȿ�� ���� ���¸� ����
    public void RegisterBulletEffect(GameObject bullet)
    {
        if (!bulletEffectStates.ContainsKey(bullet))
        {
            bulletEffectStates[bullet] = 0;
        }
        bulletEffectStates[bullet]++;

    }

    // ȿ�� ������ destroy ��� ȣ���ؾ��� destroy�� skillManager���� ����
    // �߻�ü ������� ������ ��� ȿ���� �����ķ� �����մϴ�.
    public void NotifyEffectComplete(GameObject bullet)
    {
        if (!bulletEffectStates.ContainsKey(bullet)) return;

        bulletEffectStates[bullet]--;

        if (bulletEffectStates[bullet] <= 0)
        {
            bulletEffectStates.Remove(bullet);
            Destroy(bullet);
        }
    }

    //�߻�ü ȿ���� �ִ��� �Ǻ��ϴ� �Լ��Դϴ�.
    //���� ȿ���� �����Ѵٸ� �⺻ �߻�ü�� �浹 �� ��������ʰ� ��� ȿ���� ������ ������� �˴ϴ�.
    public bool IsBulletHaveEffect(GameObject bullet)
    {
        return bulletEffectStates.ContainsKey(bullet) && bulletEffectStates[bullet] > 0;
    }
}
