using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    
    private int playerLevel = 1;
    [SerializeField]
    private float exp = 0;
    float playerSpeed = 5.0f;

    // 플레이어의 변수

    private int maxHealth = 3;
    [SerializeField]
    private int currentHealth;


    // 플레이어의 현재 총알

    private GameObject currentBullet;


    // 화면 경계를 맞춰주는 기능을 위한 변수
    Camera mainCamera;

    private Vector2 minBounds;
    private Vector2 maxBounds;

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
        if(Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(currentBullet, transform.position, Quaternion.identity);
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
        if(currentHealth > 0)
        {
            GameManger.Instance.GameOver();
        }
    }

    public void GetExpParticle(float expAmount)
    {
        exp += expAmount;

        // TODO: 레벨업 구현
    }
}
