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
        ////�Ѿ����� �±׳� ������Ʈ�� Ȯ��
        if (collision.CompareTag("PlayerBullet"))
        {
            Rigidbody2D bulletRb = collision.GetComponent<Rigidbody2D>();
            // �ӵ��� ����
            bulletRb.linearVelocity = -bulletRb.linearVelocity;
            // ȸ���� ���� ��� (�ӵ� ������ ��������)
            float angle = Mathf.Atan2(bulletRb.linearVelocity.y, bulletRb.linearVelocity.x) * Mathf.Rad2Deg;
            collision.transform.rotation = Quaternion.Euler(0, 0, angle); // +90�� �չ��� ������ (�ʿ� �� ����)
            collision.AddComponent<Knife>();

        }
    }
}
