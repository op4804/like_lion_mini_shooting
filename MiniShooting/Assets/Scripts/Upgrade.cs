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
    //�ε�����(���� ����) ���׷��̵� Ƚ�� ����
    private int[] upgradeStack;
    //Player������Ʈ�� Player��ũ��Ʈ ����
    private Player StatChange;

    //�̰ɷ� �ϸ� �� ���������� ����..
    //������ �ȵǾ��ְ� �ε��� ������ �ƴ� �뵵
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
            //���׷��̵� �޴����� ���õǸ� �� ����
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

    //���׷��̵� ��ҵ�
    void IncreaseSpeed()
    {
        //�̵��ӵ� ������
        float balancedSpeed = 0.5f;
        float t = StatChange.GetplayerSpeed() + balancedSpeed;
        StatChange.SetSpeed(t);
        upgradeStack[(int)UpgradeType.IncreaseSpeed]++;
    }

    void IncreaseFireRate()
    {
        //���� ������
        float balancedFireRate = 0.02f;
        float t = StatChange.GetFireRate() - balancedFireRate;
        StatChange.SetFireRate(t);
        upgradeStack[(int)UpgradeType.IncreaseFireRate]++;
    }

    void IncreaseMaxHealth()
    {
        //ü�� ������
        int balancedHP = 2;
        int t = StatChange.GetMaxHealth() + balancedHP;
        StatChange.SetHP(t);
        upgradeStack[(int)UpgradeType.IncreaseMaxHealth]++;
        PlayerHpBar.Instance.UpdateLife();
    }

    void IncreaseCurrentHealth()
    {
        //�����Ҷ� ü�� ȸ����
        int balancedHP = 2;
        int t = StatChange.GetCurrentHealth() + balancedHP;
        StatChange.SetCurrentHP(t);
    }

    void IncreaseBulletCount()
    {
        //�Ѿ� ���� ����
        int balancedBC = 1;
        int t = StatChange.GetbulletCount() + balancedBC;
        StatChange.SetBulletCount(t);
        upgradeStack[(int)UpgradeType.IncreaseBulletCount]++;
    }

    void IncreaseAttack()
    {
        //���ݷ� ����
        float balancedATK = 1f;
        float t = StatChange.GetAttack() + balancedATK;
        StatChange.SetAttack(t);
        upgradeStack[(int)UpgradeType.IncreaseAttack]++;
    }

    void IncreaseBulletSpeed()
    {
        //�Ѿ� �ӵ� ����
        float balancedBS = 0.5f;
        float t = StatChange.GetbulletSpeed() + balancedBS;
        StatChange.SetBulletSpeed(t);
        upgradeStack[(int)UpgradeType.IncreaseBulletSpeed]++;
    }
}
