using System.Collections.Generic;
using System;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    [HideInInspector]
    public static Upgrade Instance = null;

    private int UpgradeCount;
        
    private List<String> upgradeOptions = new List<String>();
    private int[] upgradeStack;

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
        Debug.Log(name);
    }

    public void InitializeOptions()
    {
        upgradeOptions.Add("Increase Speed");
        upgradeOptions.Add("Increase Fire Rate");
        upgradeOptions.Add("Increase Max Health");
        upgradeStack = new int[upgradeOptions.Count];
    }

}
