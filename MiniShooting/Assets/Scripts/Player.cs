using System;
using System.Collections;
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
    private int currentHealth = 6; //플레이어 현재 생명력

    private float attack = 10f; // 플레이어의 공격력

    // 플레이어의 현재 총알
    private GameObject currentBullet;
    public float fireRate = 0.2f; //연사 속도
    private float fireTimer = 0f; //다음 발사까지의 시간 계산을 위한 변수
    private int bulletCount = 1;//쏠 총알 개수
    private float bulletSpeed = 5f;

    // 화면 경계를 맞춰주는 기능을 위한 변수
    Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    // @ 수정점 1 무적 상태 여부
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;
    public float invincibleTime = 1.5f;


    public int GetPlayerLevel() => playerLevel;
    public float GetExp() => exp;
    public float GetExpScale() => expScale;
    public int GetMaxHealth() => maxHealth;
    public int GetCurrentHealth() => currentHealth;
    public float GetAttack() => attack;
    public float GetFireRate() => fireRate;
    public float GetplayerSpeed() => playerSpeed;
    public int GetbulletCount() => bulletCount;
    public float GetbulletSpeed() => bulletSpeed;

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

        // @ 수정점 2 Renderer 추가
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            FireBullets();
            fireTimer = 0f;
        }
        //누르고 있는만큼 나감
        else if (Input.GetKey(KeyCode.X))
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                FireBullets();
                fireTimer = 0f;
            }
        }
    }

    private void FireBullets()
    {
        int sign = 1;
        for (int i = 0; i < bulletCount; i++)
        {
            //총알 위아래로 순차적 생성
            sign *= -1;
            Vector3 bulletYChange = transform.position;
            bulletYChange += new Vector3(0, i * sign * 0.1f, 0);

            GameObject bullet = Instantiate(currentBullet, bulletYChange, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetBulletAttack(attack);
            bullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
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

    // @ 수정점 3 Hit 메소드에 Renderer 코루틴 추가
    public void Hit()
    {
        Invincible invSkill = GetComponent<Invincible>();
        if (invSkill != null && invSkill.IsInvincible)
        {
            Debug.Log("무적 상태 확인용 로그 2-hit 메소드 부분");
            return;
        }

        if (!isInvincible)
        {
            currentHealth--;

            if (currentHealth < 0)
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                StartCoroutine(Invisible());
            }

            PlayerHpBar.Instance.UpdateLife();
        }
    }

    // @ 수정점 4 1초 단위로 깜빡거리게 코루틴 작성
    IEnumerator Invisible()
    {
        isInvincible = true;

        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            Invincible invSkill = GetComponent<Invincible>();
            if (invSkill != null && invSkill.IsInvincible)
            {
                Debug.Log("무적 상태 확인용 로그 1-trigger");
                Destroy(collision.gameObject);
                return;
            }
            Hit();
            Destroy(collision.gameObject);
        }
    }


    public void GetExpParticle(float expAmount) //경험치 획득
    {
        exp += expAmount;

        //레벨업 구현
        if (exp % expScale == 0) // expScale의 배수마다 레벨업
        {
            playerLevel += 1;
            exp = 0;
            UIManager.Instance.ToggleUpgradeMenu(); //레벨업 능력치 상승 메뉴
        }

        UIManager.Instance.ViewExp(exp, playerLevel);  //레벨, 경험치 현황 표기

        //경험치 먹은거 체크
        //경험지 먹은지 3초 지나면 체크 해제
        expFlag = true;
        if (StopCoro != null)
        {
            StopCoroutine(StopCoro);
        }
        StopCoro = StartCoroutine(Timer());
    }

    private bool expFlag = false;
    private Coroutine StopCoro;
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
        expFlag = false;
    }
    public bool Getflag() => expFlag;




    //플레이어 스텟 변경용 함수들
    public void SetHP(int maxHealth) => this.maxHealth = maxHealth;
    public void SetCurrentHP(int currentHealth) => this.currentHealth = currentHealth;
    public void SetSpeed(float playerSpeed) => this.playerSpeed = playerSpeed;
    public void SetFireRate(float fireRate) => this.fireRate = fireRate;
    public void SetAttack(float attack) => this.attack = attack;
    public void SetBulletCount(int bulletCount) => this.bulletCount = bulletCount;
    public void SetBulletSpeed(float bulletSpeed) => this.bulletSpeed = bulletSpeed;
}
