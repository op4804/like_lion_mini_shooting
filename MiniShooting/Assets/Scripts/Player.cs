using System;
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
    private int currentHealth=6; //�÷��̾� ���� �����

    // �÷��̾��� ���� �Ѿ�
    private GameObject currentBullet;
    public float fireRate = 0.2f; //���� �ӵ�
    private float fireTimer = 0f; //���� �߻������ �ð� ����� ���� ����

    // ȭ�� ��踦 �����ִ� ����� ���� ����
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
            Instantiate(currentBullet, transform.position, Quaternion.identity);
            fireTimer = 0f;
        }
        //������ �ִ¸�ŭ ����
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

    public void GetExpParticle(float expAmount) //����ġ ȹ��
    {
        exp += expAmount;

        //������ ����
        if (exp % expScale == 0) // expScale�� ������� ������
        {
            playerLevel += 1;

            UIManager.Instance.ToggleUpgradeMenu(); //������ �ɷ�ġ ��� �޴�
        }

        UIManager.Instance.ViewExp(exp, playerLevel);  //����, ����ġ ��Ȳ ǥ��
    }
}
