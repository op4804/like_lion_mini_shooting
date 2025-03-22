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

    protected virtual void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Hit(float damage)
    {
        if (isDead) return;

        currentEnemyHP -= damage;      

        if (currentEnemyHP <= 0) // 사망
        {
            
            GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
            expParticle.GetComponent<ExperienceParticle>().SetExpAmount(10f);
            
            //크기 줄이기->줄어들면 오브젝트 파괴
            StopAllCoroutines();
            StartCoroutine(RotateAndShrinkAndDie());

            return;
        }

        gameObject.GetComponent<Animator>().SetTrigger("hit");
        transform.Translate(Vector3.right * 0.1f);
    }
    protected virtual void OnEnable()
    {
        Debug.Log("Enemy 초기화");
        isDead = false;
        transform.localScale = originalScale;
        transform.rotation = Quaternion.identity;
    }

    void OnDisable()
    {
        isDead = true;
        Debug.Log($"[Enemy] 비활성화됨: {GetInstanceID()}", this);
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

        Debug.Log("Deactivate 호출됨");
        ResourceManager.Instance.Deactivate(gameObject);
    }

}
