using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MoveTowardKnife : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    Transform playerTransform; 

    void Awake()
    {
        playerTransform = Player.Instance.transform;
    }

    void Start()
    {
        StartCoroutine(Move(playerTransform, moveSpeed));
    }

    IEnumerator Move(Transform playerTransform, float moveSpeed)
    {
        // 플레이어 방향을 향해 벡터 계산
        Vector2 dir = (playerTransform.position - transform.position).normalized;

        // 프레임마다 이동
        while (true)
        {
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(); 
            Destroy(gameObject);
        }
    }
}
