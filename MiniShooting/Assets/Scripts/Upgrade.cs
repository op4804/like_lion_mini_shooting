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
        IncreaseAttack = 4
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
        string name=upgradeOptions[selectedOption];
        switch (name)
        {
            //���׷��̵� �޴����� ���õǸ� �� ����
            case "Increase Speed":
                IncreaseSpeed();
                upgradeStack[(int)UpgradeType.IncreaseSpeed]++;
                break;
            case "Increase Fire Rate":
                IncreaseFireRate();
                upgradeStack[(int)UpgradeType.IncreaseFireRate]++;
                break;
            case "Increase Max Health":
                IncreaseMaxHealth();
                upgradeStack[(int)UpgradeType.IncreaseMaxHealth]++;
                break;
            case "Increase Bullet Count":
                IncreaseBulletCount();
                upgradeStack[(int)UpgradeType.IncreaseBulletCount]++;
                break;
            case "Increase Attack":
                upgradeStack[(int)UpgradeType.IncreaseAttack]++;
                IncreaseAttack();
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
        upgradeStack = new int[upgradeOptions.Count];
        StatChange = Player.GetComponent<Player>();
    }

    //���׷��̵� ��ҵ�
    void IncreaseSpeed()
    {
        //�̵��ӵ� ������
        float balancedSpeed = 0.5f;
        StatChange.SetSpeed(balancedSpeed);
    }

    void IncreaseFireRate()
    {
        //���� ������
        float balancedFireRate = 0.02f;
        StatChange.SetFireRate(balancedFireRate);
    }

    void IncreaseMaxHealth()
    {
        //ü�� ������
        int balancedHP = 1;
        StatChange.SetHP(balancedHP);
    }

    void IncreaseBulletCount()
    {
        //�Ѿ� ���� ����
        int balancedBC = 1;
        StatChange.SetBulletCount(balancedBC);
    }

    void IncreaseAttack()
    {
        //���ݷ� ����
        int balancedATK = 1;
        StatChange.SetAttack(balancedATK);
    }
}
