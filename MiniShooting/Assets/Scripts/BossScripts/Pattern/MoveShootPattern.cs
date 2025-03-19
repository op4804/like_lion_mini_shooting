using UnityEngine;

public class MoveShootPattern : MonoBehaviour
{
    public GameObject knifePrefab;//던질 칼
    public Transform knifeSpawnPoint;//칼 던지는 위치

    public float moveSpeed = 10f;//칼의 속도
    public float yRange = 4f;//보스 가동범위
    public float shootInterval = 0.25f;//칼던지는 딜레이

    protected float shootTimer;
    protected Vector2 startPos;
    protected int direction = 1;

    void Start()
    {
        startPos = transform.position;
        shootTimer = shootInterval;
    }

    void Update()
    {
        MoveVertical();
        HandleShooting();
    }

    void MoveVertical()
    {
        transform.Translate(Vector2.up * direction * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y - startPos.y) > yRange)
        {
            direction *= -1;
        }
    }

    void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            ShootKnife();
            shootTimer = shootInterval;
        }
    }

    void ShootKnife()
    {
        GameObject knife = Instantiate(knifePrefab, knifeSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = knife.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.left * 8f;
    }
}
