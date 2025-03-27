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
            //skill.ApplyEffect(); //��ų�� ȿ�� ���

            skill.LockSkill();
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
                else
                    Debug.Log($"Ȱ��ȭ�� ��Ƽ�� ��ų�� �����ϴ�.");
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

        if (!bulletEffectStates[bullet].Contains(effectName))
        {
            bulletEffectStates[bullet].Add(effectName);
            Debug.Log($"{bullet.name}�� {effectName} ȿ�� ��ϵ�", bullet);
            Debug.Log($"[RegisterBulletEffect] {bullet.name} - ���� ���� ȿ�� ��: {bulletEffectStates[bullet].Count}", bullet);
        }
        else
        {
            Debug.LogWarning($"{bullet.name}�� {effectName} ȿ�� �ߺ� ��� �õ�", bullet);
        }
    }

    // ȿ�� ������ destroy ��� ȣ���ؾ��� destroy�� skillManager���� ����
    // �߻�ü ������� ������ ��� ȿ���� �����ķ� �����մϴ�.
    public void NotifyEffectComplete(GameObject bullet, string effectName)
    {
        Debug.Log($"[NotifyEffectComplete] ȣ��� - bullet: {bullet.name}, effect: {effectName}");

        if (!bulletEffectStates.ContainsKey(bullet))
        {
            Debug.LogWarning($"[NotifyEffectComplete] bulletEffectStates�� {bullet.name} ��� �ȵ�!", bullet);
            return;
        }

        //if (!bulletEffectStates.ContainsKey(bullet)) return;

        bulletEffectStates[bullet].Remove(effectName);

        Debug.Log($"{bullet.name}�� {effectName} ȿ�� ���� �õ�", bullet);
        Debug.Log($"[NotifyEffectComplete] {bullet.name} - ���� ���� ȿ�� ��: {bulletEffectStates[bullet].Count}", bullet);

        if (bulletEffectStates[bullet].Count == 0)
        {
            bulletEffectStates.Remove(bullet);
            ResourceManager.Instance.Deactivate(bullet);
        }
    }

    public void NotifyEffectComplete(GameObject bullet)
    {
        ResourceManager.Instance.Deactivate(bullet);
    }

    //ȭ�� ������ ��Ż�� ȿ���� �������ִ� �Լ��Դϴ�.
    public void NotifyAllEffectsComplete(GameObject bullet)
    {
        if (!bulletEffectStates.ContainsKey(bullet)) return;

        bulletEffectStates.Remove(bullet);
        Debug.Log($"{bullet.name}�� ��� ȿ�� ���ŵ� (ȭ�� ��Ż)", bullet);
    }

    //�߻�ü ȿ���� �ִ��� �Ǻ��ϴ� �Լ��Դϴ�.
    //���� ȿ���� �����Ѵٸ� �⺻ �߻�ü�� �浹 �� ��������ʰ� ��� ȿ���� ������ ������� �˴ϴ�.
    public bool IsBulletHaveEffect(GameObject bullet)
    {
        return bulletEffectStates.ContainsKey(bullet) && bulletEffectStates[bullet].Count > 0;
    }

    public void SetActiveSkill(Skill selectSkill)
    {
        Debug.Log("��Ƽ�� ��ų�� �����մϴ�.");
        foreach (var skill in skills)
        {
            skill.LockSkill();
        }
        selectSkill.UnlockSkill();
        var activeUnlockedSkill = skills.Find(skill => skill.IsActiveSkill && skill.IsUnlockSkill());
        Debug.Log($"���� ���õ� ��ų : {activeUnlockedSkill.name}");
    }

    public void SetPassiveSkill(Skill selectSkill)
    {
        Debug.Log("�нú� ��ų�� �����մϴ�.");
        selectSkill.UnlockSkill();
        var passiveUnlockedSkill = skills.FindAll(skill => !skill.IsActiveSkill && skill.IsUnlockSkill());

        foreach (var s in passiveUnlockedSkill)
        {
            Debug.Log($"���� ���õ� ��ų : {s.name}");
        }
    }
}
