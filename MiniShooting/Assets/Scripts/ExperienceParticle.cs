using UnityEngine;

public class ExperienceParticle : MonoBehaviour
{

    private float moveSpeed = 5f; // 플레이어에게 날아가는 속도
    private float attractionRange = 3f; // 플레이어에게 날아가기 시작하는 거리

    private float expAmount = 0f;
    private Transform player;
    private bool isAttracted = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        
        // 플레이어가 특정 거리 안에 있으면 자동으로 따라가도록 설정
        if (distance < attractionRange)
        {
            isAttracted = true;
        }

        if (isAttracted)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += new Vector3(-0.5f * Time.deltaTime, 0,0);
        }
    }

    public void SetExpAmount(float amount)
    {
        expAmount = amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GetExpParticle(expAmount);
            Destroy(gameObject);
        }
    }
}
