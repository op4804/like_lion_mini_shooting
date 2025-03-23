using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

// 기본적으로는 그냥 복사 붙여넣기하고 표기한 변수만 고쳐주시면됩니다.
// 스킬 구현부-> 스킬 -> 스킬매니저순으로 진행됩니다.
// 해당 클래스는 스킬 구현부입니다.
// 실제 물리적인 계산, 효과, 이펙트등 모든 구현이 이 클래스에 속합니다.
// 책의 속지, 내용이라고 생각하시면 될것같습니다.
// 구현부에서는 필수적으로 구현해야하는 함수는 없습니다.
// 이펙트가 있다면 exoplosion의 이펙트 부분을 참고하세요.

// 기본적으로 bullet에 붙어서 쓰인다고 생각하시고 구현하시면됩니다.
// 기본 발사체의 움직임빼고 충돌이나 기타 특성은 무시하시고 그냥 실제 어떻게 작동하는지만 구현하시면됩니다.
// 예를 들어 발사체의 앞으로 나가는 특성은 그대로 가지고있으니 따로 구현안하셔도되고
// 부딪혔을때 충돌처리도 스킬매니저가 해주기때문에
// 효과만 붙이면 알아서 기본 발사체의 로직은 무시됩니다.

public class BouncingBullet : MonoBehaviour
{
    private int maxBounces = 3; // 최대 튕기는 횟수
    private int currentBounce = 0; // 튕긴 횟수 카운트
    private float bounceRadius = 200f; // 튕기는 범위    
    private bool isFirst = false; // 첫 충돌 여부

    private GameObject currentTarget; // 다음 타겟 저장

    private List<GameObject> alreadyHit = new List<GameObject>(); // 맞은적 중복 제거

    private Rigidbody2D rb; // 첫충돌 이후의 발사체 이동을 위해 사용하는 변수입니다.

    // 스킬부의 고유값을 가져와 구현을 위해 세팅해주는 함수입니다.
    public void SetBounceValues(int maxBounces, float bounceRadius)
    {
        this.maxBounces = maxBounces;
        this.bounceRadius = bounceRadius;
    }
    void OnEnable() // 오브젝트풀 관련 발사체 초기화코드입니다. 필요하시면 넣으시면됩니다.
    {
        currentBounce = 0;
        isFirst = false;
        currentTarget = null;
        alreadyHit.Clear(); // List 초기화
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // rb 초기화
    }

    private void FixedUpdate()
    {
        if (isFirst == true)
        {
            if (currentTarget == null || !currentTarget || currentTarget.GetComponent<Enemy>()?.IsDead() == true)
            {
                SkillManager.Instance.NotifyEffectComplete(gameObject, name); // destroy 기능을 한다고 생각하시면 됩니다. 로직중 어딘가에 반드시 구현되어야합니다.
                                                                              // 실제 destroy는 아니고 스킬매니저에게 효과를 끝내도 된다 알려주는 함수입니다.
                                                                              // 온, 오프로 생각했을때 스킬 효과를 오프해주는 함수라고 생각하시면됩니다.
                return;
            }
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * Player.Instance.GetbulletSpeed(); // 다음 타겟으로 이동
        }
    }



    // 이 밑으로는 바운스의 구현부이니 다 지우시고 구현하시면 됩니다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // 충돌이 적인지 판단
        {
            var enemy = collision.gameObject; // 충돌 객체를 저장

            if (alreadyHit.Contains(enemy)) return; // 이미 맞은 적이라면 무시

            enemy.GetComponent<Enemy>().Hit(Player.Instance.GetAttack()); // 데미지 처리

            alreadyHit.Add(enemy); //이미 맞은적으로 저장
            isFirst = true; //첫 충돌 활성화
            currentBounce++; //충돌횟수 증가
            //Debug.Log($"{GetInstanceID()}가 {currentBounce}번째 타격 : {collision}, ID: {collision.GetInstanceID()}", this);

            if (currentBounce >= maxBounces)
            {
                SkillManager.Instance.NotifyEffectComplete(gameObject, name);
            }
            NextTarget(enemy.transform.position);//다음 타겟 찾는 함수 호출(충돌 객체 좌표 기준)
        }
    }

    private void NextTarget(Vector3 currentPosition)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(currentPosition, bounceRadius); //객체 기준 bounceRadius만큼 검색해서 객체를 가져옴
        GameObject nextTarget = null; //다음 타겟 변수 선언
        float closestDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy") && !alreadyHit.Contains(hit.gameObject)) //이미 맞은 객체 식별, 검색한 객체를 적인지 판단
            {
                Enemy enemy = hit.GetComponent<Enemy>();

                if (enemy != null && !enemy.IsDead())
                {
                    float distance = Vector3.Distance(currentPosition, hit.transform.position); //타겟과의 거리 계산

                    //범위 안의 타겟중 가장 가까운 타겟을 선정
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nextTarget = hit.gameObject;
                    }
                }
            }
        }

        if (nextTarget != null)
        {
            currentTarget = nextTarget;
            // Debug.Log($"{GetInstanceID()}의 다음 대상 : {nextTarget}, ID: {nextTarget.GetInstanceID()}", this);
        }

        else
        {
            // Debug.Log($"타겟 없음 종료", this);
            SkillManager.Instance.NotifyEffectComplete(gameObject, name); // 유효한 타겟이 없으면 바로 종료
        }
    }
}
