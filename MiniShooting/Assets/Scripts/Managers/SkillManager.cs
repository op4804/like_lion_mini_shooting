using UnityEngine;
using System.Collections.Generic;
using System;

//스킬 구현부-> 스킬 -> 스킬매니저순으로 진행됩니다.
//해당 클래스는 스킬들을 관리해주는 스킬매니저입니다.
//스킬의 효과를 온, 오프를 해주고 등록해주는 기능을 합니다.
//책들이 들어가있고 관리해주는 사서가있는 도서관이라고 생각하시면 될것같습니다.

//스킬 매니저에서 따로 설정해야하는 사항은 없습니다.
//스킬이 구현되었으면 하이라키에서 오브젝트에 스크립트를 등록해주고
//스킬매니저의 skills에 +를 눌러 변수칸을 추가해 해당 오브젝트를 넣어주면됩니다.

//패시브 스킬
//스킬의 활성화 여부만 바꿔주면 활성화된 기존 효과와 같이 바로 중첩 적용됩니다.
//켜진 스킬이 없다면 기본 발사체가 발사됩니다.

//액티브 스킬
//반드시 하나의 효과만 켜주어야합니다.

public class SkillManager : MonoBehaviour
{
    [HideInInspector]
    public static SkillManager Instance = null;

    public GameObject defaultBulletPrefab;
    private GameObject currentBulletPrefab;

    public List<Skill> skills = new List<Skill>(); //스킬 등록
    private List<Action<GameObject>> bulletModifiers = new List<Action<GameObject>>(); //패시브 스킬 적용

    // 발사체별 효과 카운트 저장
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

        currentBulletPrefab = defaultBulletPrefab; // 기본 발사체의 효과를 가짐
    }

    private void Start()
    {
        foreach (var skill in skills)
        {
            skill.InitializeSkill(Player.Instance); //플레이어 정보를 가져옴
            //skill.ApplyEffect(); //스킬의 효과 등록

            skill.LockSkill();
        }

        Debug.Log($"보유 스킬 수 : {skills.Count}");
    }

    private void Update()
    {
        if (skills.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.Z)) //z키를 눌러 액티브 스킬 사용
        {
            foreach (var skill in skills)
            {
                if (skill.IsActiveSkill && skill.IsUnlockSkill())
                    skill.UseSkill();
                else
                    Debug.Log($"활성화된 액티브 스킬이 없습니다.");
            }
        }
    }

    //발사체 효과 등록
    public void AddBulletModifier(Action<GameObject> modifier)
    {
        bulletModifiers.Add(modifier);
    }

    // 발사체 생성 + 효과 적용
    public GameObject CreateBullet(Vector3 position)
    {
        if (currentBulletPrefab == null) return null;

        GameObject bullet = ResourceManager.Instance.Create(currentBulletPrefab.name, position);

        foreach (var modifier in bulletModifiers)
        {
            modifier.Invoke(bullet); //기존 발사체에 켜진 효과를 적용 시켜줍니다.
        }

        return bullet;
    }

    //발사체 효과 생성시 호출해야함. 발사하는 발사체의 효과 진행 상태를 저장
    public void RegisterBulletEffect(GameObject bullet, string effectName)
    {
        if (!bulletEffectStates.ContainsKey(bullet))
            bulletEffectStates[bullet] = new HashSet<string>();

        if (!bulletEffectStates[bullet].Contains(effectName))
        {
            bulletEffectStates[bullet].Add(effectName);
            Debug.Log($"{bullet.name}에 {effectName} 효과 등록됨", bullet);
            Debug.Log($"[RegisterBulletEffect] {bullet.name} - 현재 남은 효과 수: {bulletEffectStates[bullet].Count}", bullet);
        }
        else
        {
            Debug.LogWarning($"{bullet.name}에 {effectName} 효과 중복 등록 시도", bullet);
        }
    }

    // 효과 끝날때 destroy 대신 호출해야함 destroy를 skillManager에서 관리
    // 발사체 사라지는 시점을 모든 효과가 끝난후로 관리합니다.
    public void NotifyEffectComplete(GameObject bullet, string effectName)
    {
        Debug.Log($"[NotifyEffectComplete] 호출됨 - bullet: {bullet.name}, effect: {effectName}");

        if (!bulletEffectStates.ContainsKey(bullet))
        {
            Debug.LogWarning($"[NotifyEffectComplete] bulletEffectStates에 {bullet.name} 등록 안됨!", bullet);
            return;
        }

        //if (!bulletEffectStates.ContainsKey(bullet)) return;

        bulletEffectStates[bullet].Remove(effectName);

        Debug.Log($"{bullet.name}에 {effectName} 효과 삭제 시도", bullet);
        Debug.Log($"[NotifyEffectComplete] {bullet.name} - 현재 남은 효과 수: {bulletEffectStates[bullet].Count}", bullet);

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

    //화면 밖으로 이탈시 효과를 제거해주는 함수입니다.
    public void NotifyAllEffectsComplete(GameObject bullet)
    {
        if (!bulletEffectStates.ContainsKey(bullet)) return;

        bulletEffectStates.Remove(bullet);
        Debug.Log($"{bullet.name}의 모든 효과 제거됨 (화면 이탈)", bullet);
    }

    //발사체 효과가 있는지 판별하는 함수입니다.
    //켜진 효과가 존재한다면 기본 발사체의 충돌 후 사라지지않고 모든 효과가 끝나고 사라지게 됩니다.
    public bool IsBulletHaveEffect(GameObject bullet)
    {
        return bulletEffectStates.ContainsKey(bullet) && bulletEffectStates[bullet].Count > 0;
    }

    public void SetActiveSkill(Skill selectSkill)
    {
        Debug.Log("액티브 스킬을 세팅합니다.");
        foreach (var skill in skills)
        {
            skill.LockSkill();
        }
        selectSkill.UnlockSkill();
        var activeUnlockedSkill = skills.Find(skill => skill.IsActiveSkill && skill.IsUnlockSkill());
        Debug.Log($"현재 세팅된 스킬 : {activeUnlockedSkill.name}");
    }

    public void SetPassiveSkill(Skill selectSkill)
    {
        Debug.Log("패시브 스킬을 세팅합니다.");
        selectSkill.UnlockSkill();
        var passiveUnlockedSkill = skills.FindAll(skill => !skill.IsActiveSkill && skill.IsUnlockSkill());

        foreach (var s in passiveUnlockedSkill)
        {
            Debug.Log($"현재 세팅된 스킬 : {s.name}");
        }
    }
}
