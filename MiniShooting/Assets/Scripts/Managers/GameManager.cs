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

    // 화면 경계를 맞춰주는 기능을 위한 변수
    Camera mainCamera;
    [HideInInspector]
    public Vector2 minBounds;
    public Vector2 maxBounds;

    // 플레이어
    public GameObject player;

    private void Start()
    {
        // 화면 경계
        mainCamera = Camera.main;

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBounds = new Vector2(topRight.x, topRight.y);
    }

    private void Update()
    {
        //임시로 레벨업UI 활성화버튼
        if (Input.GetKeyDown(KeyCode.F1))
        {
            UIManager.Instance.ToggleUpgradeMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.ToggleStatusMenu();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            UIManager.Instance.ToggleSkillMenu();
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        SoundManager.instance.PlayerDie(); // 플레이어 사망 시 재생
    }
}
