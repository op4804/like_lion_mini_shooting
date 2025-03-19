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
        string name=upgradeOptions[selectedOption];
        switch (name)
        {
            case "Increase Speed":
                IncreaseSpeed();
                upgradeStack[0]++;
                Debug.Log(upgradeStack[0]);
                break;
            case "Increase Fire Rate":
                IncreaseFireRate();
                upgradeStack[1]++;
                break;
            case "Increase Max Health":
                IncreaseMaxHealth();
                upgradeStack[2]++;
                break;
            case "More Bullet":
                upgradeStack[3]++;
                break;
            case "Increase Attack":
                upgradeStack[4]++;
                break;
        }
    }

    
    public void InitializeOptions()
    {
        upgradeOptions.Add("Increase Speed");
        upgradeOptions.Add("Increase Fire Rate");
        upgradeOptions.Add("Increase Max Health");
        upgradeOptions.Add("Increase Attack");
        upgradeOptions.Add("More Bullet");
        upgradeStack = new int[upgradeOptions.Count];
        StatChange = Player.GetComponent<Player>();
    }




    //업그레이드 요소들
    void IncreaseSpeed()
    {
        //이동속도 증가량
        float valancedSpeed = 0.5f;
        StatChange.SetSpeed(valancedSpeed);
    }

    void IncreaseFireRate()
    {
        //공속 증가량
        float valancedFireRate = 0.02f;
        StatChange.SetFireRate(valancedFireRate);
    }

    void IncreaseMaxHealth()
    {
        //체력 증가량
        int valancedHP = 1;
        StatChange.SetHP(valancedHP);
    }
}
