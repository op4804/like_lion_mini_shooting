using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class Upgrade : MonoBehaviour
{
    public GameObject Player;

    [HideInInspector]
    public static Upgrade Instance = null;

    private List<String> upgradeOptions = new List<String>();
    //인덱스별(넣은 순서) 업그레이드 횟수 저장
    private int[] upgradeStack;
    //Player오브젝트의 Player스크립트 역할
    private Player StatChange;

    //이걸로 하면 더 복잡해지는 느낌..
    //연결은 안되어있고 인덱스 순서만 아는 용도
    public enum UpgradeType
    {
        IncreaseSpeed = 0,
        IncreaseFireRate = 1,
        IncreaseMaxHealth = 2,
        IncreaseBulletCount= 3,
        IncreaseAttack = 4,
        IncreaseBulletSpeed = 5,
    }

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
    InitializeOptions();
    }

    public int GetCount()
    {
        return upgradeOptions.Count;
    }

    public string GetUpgradeString(int i)
    {
        return upgradeOptions[i];
    }
        
    public void UpgradeStack(int selectedOption)
    {
        Debug.Log(upgradeOptions[selectedOption]);
        UpgradeType upgradeType = (UpgradeType)selectedOption;

        switch (upgradeType)
        {
            //업그레이드 메뉴에서 선택되면 값 변경
            case UpgradeType.IncreaseSpeed:
                IncreaseSpeed();
                break;
            case UpgradeType.IncreaseFireRate:
                IncreaseFireRate();
                break;
            case UpgradeType.IncreaseMaxHealth:
                IncreaseCurrentHealth();
                IncreaseMaxHealth();
                break;
            case UpgradeType.IncreaseBulletCount:
                IncreaseBulletCount();
                break;
            case UpgradeType.IncreaseAttack:
                IncreaseAttack();
                break;
            case UpgradeType.IncreaseBulletSpeed:
                IncreaseBulletSpeed();
                break;
        }
    }
    
    public void InitializeOptions()
    {
        upgradeOptions.Add("Increase Speed");
        upgradeOptions.Add("Increase Fire Rate");
        upgradeOptions.Add("Increase Max Health");
        upgradeOptions.Add("Increase Bullet Count");
        upgradeOptions.Add("Increase Attack");
        upgradeOptions.Add("Increase Bullet Speed");
        upgradeStack = new int[upgradeOptions.Count];
        StatChange = Player.GetComponent<Player>();
    }

    //업그레이드 요소들
    void IncreaseSpeed()
    {
        //이동속도 증가량
        float balancedSpeed = 0.5f;
        float t = StatChange.GetplayerSpeed() + balancedSpeed;
        StatChange.SetSpeed(t);
        upgradeStack[(int)UpgradeType.IncreaseSpeed]++;
    }

    void IncreaseFireRate()
    {
        //공속 증가량
        float balancedFireRate = 0.02f;
        float t = StatChange.GetFireRate() - balancedFireRate;
        StatChange.SetFireRate(t);
        upgradeStack[(int)UpgradeType.IncreaseFireRate]++;
    }

    void IncreaseMaxHealth()
    {
        //체력 증가량
        int balancedHP = 2;
        int t = StatChange.GetMaxHealth() + balancedHP;
        StatChange.SetHP(t);
        upgradeStack[(int)UpgradeType.IncreaseMaxHealth]++;
        PlayerHpBar.Instance.UpdateLife();
    }

    void IncreaseCurrentHealth()
    {
        //업글할때 체력 회복용
        int balancedHP = 2;
        int t = StatChange.GetCurrentHealth() + balancedHP;
        StatChange.SetCurrentHP(t);
    }

    void IncreaseBulletCount()
    {
        //총알 개수 증가
        int balancedBC = 1;
        int t = StatChange.GetbulletCount() + balancedBC;
        StatChange.SetBulletCount(t);
        upgradeStack[(int)UpgradeType.IncreaseBulletCount]++;
    }

    void IncreaseAttack()
    {
        //공격력 증가
        float balancedATK = 1f;
        float t = StatChange.GetAttack() + balancedATK;
        StatChange.SetAttack(t);
        upgradeStack[(int)UpgradeType.IncreaseAttack]++;
    }

    void IncreaseBulletSpeed()
    {
        //총알 속도 증가
        float balancedBS = 0.5f;
        float t = StatChange.GetbulletSpeed() + balancedBS;
        StatChange.SetBulletSpeed(t);
        upgradeStack[(int)UpgradeType.IncreaseBulletSpeed]++;
    }
}
