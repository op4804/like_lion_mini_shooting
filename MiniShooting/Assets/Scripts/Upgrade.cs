using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class Upgrade : MonoBehaviour
{

    [HideInInspector]
    public static Upgrade Instance = null;

    private int UpgradeCount;
        
    private List<String> upgradeOptions = new List<String>();
    private int[] upgradeStack;
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
    StatChange = GetComponent<Player>();
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
                break;
            case "Increase Fire Rate":
                IncreaseFireRate();
                break;
            case "Increase Max Health":
                IncreaseMaxHealth();
                break;
        }
    }

    public void InitializeOptions()
    {
        upgradeOptions.Add("Increase Speed");
        upgradeOptions.Add("Increase Fire Rate");
        upgradeOptions.Add("Increase Max Health");
        upgradeStack = new int[upgradeOptions.Count];
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
