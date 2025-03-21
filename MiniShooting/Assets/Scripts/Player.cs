using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public static Player Instance = null;

    private int playerLevel = 1; //�÷��̾� �ʱ� ����
    private int expScale = 100; //����ġ �����ϸ� ��ġ

    [SerializeField]
    private float exp = 0; // �÷��̾� �ʱ� ����ġ
    private float playerSpeed = 5.0f; //�÷��̾� ���ǵ�

    // �÷��̾��� ����
    private int maxHealth = 6; //�÷��̾� �ִ� �����
    [SerializeField]
    private int currentHealth = 6; //�÷��̾� ���� �����

    private float attack = 10f; // �÷��̾��� ���ݷ�

    // �÷��̾��� ���� �Ѿ�
    private GameObject currentBullet;
    public float fireRate = 0.2f; //���� �ӵ�
    private float fireTimer = 0f; //���� �߻������ �ð� ����� ���� ����
    private int bulletCount = 1;//�� �Ѿ� ����
    private float bulletSpeed = 5f;

    // ȭ�� ��踦 �����ִ� ����� ���� ����
    Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    // @ ������ 1 ���� ���� ����
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
        // ȭ�� ���
        mainCamera = Camera.main;

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBounds = new Vector2(topRight.x, topRight.y);

        // TODO: ī�޶� ���� �� ȭ���� ��찪�� �÷��̾ ����ϴ� ������ �ƴϴϱ� ��ġ �̵��� �ؾ��ҵ�. 

        // ü�� �ʱ�ȭ
        currentHealth = maxHealth;

        // �Ѿ� �ʱ�ȭ
        currentBullet = ResourceManager.Instance.playerBullet1;

        // @ ������ 2 Renderer �߰�
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        FitBoundary(); // ��踦 �Ѿ�� �ʰ� ���ִ� �Լ�.
        Attack();
    }

    private void Attack()
    {
        //ó�� ������ ������ ���� �߻�
        if (Input.GetKeyDown(KeyCode.X))
        {
            FireBullets();
            fireTimer = 0f;
        }
        //������ �ִ¸�ŭ ����
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
            //�Ѿ� ���Ʒ��� ������ ����
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

    // @ ������ 3 Hit �޼ҵ忡 Renderer �ڷ�ƾ �߰�
    public void Hit()
    {
        Invincible invSkill = GetComponent<Invincible>();
        if (invSkill != null && invSkill.IsInvincible)
        {
            Debug.Log("���� ���� Ȯ�ο� �α� 2-hit �޼ҵ� �κ�");
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

    // @ ������ 4 1�� ������ �����Ÿ��� �ڷ�ƾ �ۼ�
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
                Debug.Log("���� ���� Ȯ�ο� �α� 1-trigger");
                Destroy(collision.gameObject);
                return;
            }
            Hit();
            Destroy(collision.gameObject);
        }
    }


    public void GetExpParticle(float expAmount) //����ġ ȹ��
    {
        exp += expAmount;

        //������ ����
        if (exp % expScale == 0) // expScale�� ������� ������
        {
            playerLevel += 1;
            exp = 0;
            UIManager.Instance.ToggleUpgradeMenu(); //������ �ɷ�ġ ��� �޴�
        }

        UIManager.Instance.ViewExp(exp, playerLevel);  //����, ����ġ ��Ȳ ǥ��

        //����ġ ������ üũ
        //������ ������ 3�� ������ üũ ����
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




    //�÷��̾� ���� ����� �Լ���
    public void SetHP(int maxHealth) => this.maxHealth = maxHealth;
    public void SetCurrentHP(int currentHealth) => this.currentHealth = currentHealth;
    public void SetSpeed(float playerSpeed) => this.playerSpeed = playerSpeed;
    public void SetFireRate(float fireRate) => this.fireRate = fireRate;
    public void SetAttack(float attack) => this.attack = attack;
    public void SetBulletCount(int bulletCount) => this.bulletCount = bulletCount;
    public void SetBulletSpeed(float bulletSpeed) => this.bulletSpeed = bulletSpeed;
}
