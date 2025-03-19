using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance = null;

    private bool isGameOver;
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
    }

    // ȭ�� ��踦 �����ִ� ����� ���� ����
    Camera mainCamera;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private void Start()
    {
        // ȭ�� ���
        mainCamera = Camera.main;

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBounds = new Vector2(topRight.x, topRight.y);
    }

    private void Update()
    {
        //�ӽ÷� ������UI Ȱ��ȭ��ư
        if (Input.GetKeyDown(KeyCode.F1))
        {
            UIManager.Instance.ToggleUpgradeMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.ToggleStatusMenu();
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
