using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    // 몬스터 체력
    [SerializeField]
    protected float currentEnemyHP;
    private float MaxEnemyHp;
    private Vector3 originalScale;

    private bool isDead = false;
    public bool IsDead() => isDead;

    protected virtual void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Hit(float damage)
    {
        if (isDead) return; // 죽었으면 피격되지 않음.

        currentEnemyHP -= damage;      

        if (currentEnemyHP <= 0) // 사망
        {
            isDead = true;

            GetComponent<Collider2D>().enabled = false;

            GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
            expParticle.GetComponent<ExperienceParticle>().SetExpAmount(10f);
            
            // 크기 줄이기-> 줄어들면 오브젝트 파괴
            StopAllCoroutines(); // 모든 행동 중지
            StartCoroutine(RotateAndShrinkAndDie());

            return;
        }

        gameObject.GetComponent<Animator>().SetTrigger("hit");
        transform.Translate(Vector3.right * 0.1f);
    }
    protected virtual void OnEnable() // 초기화
    {
        GetComponent<Collider2D>().enabled = true;
        isDead = false;
        transform.localScale = originalScale;
        transform.rotation = Quaternion.identity;
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    //원심분리기
    IEnumerator RotateAndShrinkAndDie()
    {

        float duration = 0.3f;

        float rotated = 0f;
        float rotateSpeed = 720f / duration;

        float scaled = 0f;
        float shrinkSpeed = 1f / duration;

        Vector3 originalScale = transform.localScale;

        while (rotated < 360f || scaled < 1f)
        {
            float delta = Time.deltaTime;

            // 회전
            float rotationThisFrame = rotateSpeed * delta;
            transform.Rotate(Vector3.up, rotationThisFrame, Space.Self);
            rotated += rotationThisFrame;

            // 축소
            float shrinkThisFrame = shrinkSpeed * delta;
            float t = scaled + shrinkThisFrame;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            scaled = t;

            yield return null;
        }

        transform.localScale = Vector3.zero;

        ResourceManager.Instance.Deactivate(gameObject);
    }

}
