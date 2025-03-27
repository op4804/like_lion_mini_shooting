using Unity.VisualScripting;
using UnityEngine;

public class BossBarrier : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ////총알인지 태그나 컴포넌트로 확인
        if (collision.CompareTag("PlayerBullet"))
        {
            Rigidbody2D bulletRb = collision.GetComponent<Rigidbody2D>();
            // 속도를 반전
            bulletRb.velocity = -bulletRb.velocity;
            // 회전값 반전 계산 (속도 방향을 기준으로)
            float angle = Mathf.Atan2(bulletRb.velocity.y, bulletRb.velocity.x) * Mathf.Rad2Deg;
            collision.transform.rotation = Quaternion.Euler(0, 0, angle); // +90은 앞방향 보정용 (필요 시 조절)
            collision.AddComponent<Knife>();

        }
    }
}
