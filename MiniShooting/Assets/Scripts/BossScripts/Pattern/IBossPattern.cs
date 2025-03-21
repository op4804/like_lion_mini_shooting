using UnityEngine;

public interface IBossPattern
{
    void StartPattern(); // 패턴 실행
    void StopPattern(); // 패턴 중지

}
//Vector2 direction = (playerTransform.position - transform.position).normalized;
//transform.Translate(direction * dashSpeed * );