using UnityEngine;

public interface IBossPattern
{
    void StartPattern(); // ���� ����
    void StopPattern(); // ���� ����

}
//Vector2 direction = (playerTransform.position - transform.position).normalized;
//transform.Translate(direction * dashSpeed * );