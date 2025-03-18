using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public static Player Instance = null;

    private int playerLevel = 1; //플레이어 초기 레벨
    private int expScale = 100; //경험치 스케일링 수치

    [SerializeField]
    private float exp = 0; // 플레이어 초기 경험치
    private float playerSpeed = 5.0f; //플레이어 스피드

    // 플레이어의 변수
    private int maxHealth = 6; //플레이어 최대 생명력
    [SerializeField]
    private int currentHealth=6; //플레이어 현재 생명력

    // 플레이어의 현재 총알
    private GameObject currentBullet;
    public float fireRate = 0.2f; //연사 속도
    private float fireTimer = 0f; //다음 발사까지의 시간 계산을 위한 변수

    // 화면 경계를 맞춰주는 기능을 위한 변수
    Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    public int GetMaxHealth() => maxHealth;
    public int GetCurrentHealth() => currentHealth;
    public int GetPlayerLevel() => playerLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 화면 경계
        mainCamera = Camera.main;

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBounds = new Vector2(topRight.x, topRight.y);

        // TODO: 카메라 정보 및 화면의 경곗값은 플레이어만 사용하는 정보는 아니니까 위치 이동을 해야할듯. 

        // 체력 초기화
        currentHealth = maxHealth;

        // 총알 초기화
        currentBullet = ResourceManager.Instance.playerBullet1;
    }

    void Update()
    {
        Move();
        FitBoundary(); // 경계를 넘어가지 않게 해주는 함수.
        Attack();
    }

    private void Attack()
    {
        //처음 누를때 딜레이 없이 발사
        if (Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(currentBullet, transform.position, Quaternion.identity);
            fireTimer = 0f;
        }
        //누르고 있는만큼 나감
        if (Input.GetKey(KeyCode.X))
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                Instantiate(currentBullet, transform.position, Quaternion.identity);
                fireTimer = 0f;
            }
        }
    }

    private void Move()
    {
        transform.Translate(Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime, Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime, 0);
    }

    private void FitBoundary()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        transform.position = newPosition;
    }

    public void Hit()
    {
        currentHealth--;

        if (currentHealth < 0)
        {
            GameManager.Instance.GameOver();
        }

        PlayerHpBar.Instance.UpdateLife();
    }

    public void GetExpParticle(float expAmount) //경험치 획득
    {
        exp += expAmount;

        //레벨업 구현
        if (exp % expScale == 0) // expScale의 배수마다 레벨업
        {
            playerLevel += 1;

            UIManager.Instance.ToggleUpgradeMenu(); //레벨업 능력치 상승 메뉴
        }

        UIManager.Instance.ViewExp(exp, playerLevel);  //레벨, 경험치 현황 표기
    }
}
