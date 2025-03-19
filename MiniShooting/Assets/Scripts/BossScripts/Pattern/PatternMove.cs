using UnityEngine;

public class PatternMove : MonoBehaviour
{
    public float moveSpeed = 10f;       
    public float yRange = 4f;
    public float shootInterval = 0.25f;
    public GameObject knifePrefab;    
    public Transform knifeSpawnPoint;  

    private float shootTimer;
    private Vector2 startPos;
    private int direction = 1;

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
            direction *= -1; // ���� ����
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
        rb.linearVelocity = Vector2.left * 8f;  // �������� ���� �߻�
    }
}
