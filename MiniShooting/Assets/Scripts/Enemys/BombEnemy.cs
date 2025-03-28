using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class BombEnemy : Enemy
{
    Transform target; // 따라가는 오브젝트의 위치 (플레이어)
    float distance = 1.2f; // 플레이어 ditance까지 쫓아감.
    float speed = 1;
    bool isTracing = true; // 따라가는 중

    void Start()
    {
        target = GameManager.Instance.player.transform;
    }

    protected override void OnEnable() // 초기화
    {
        base.OnEnable();

        gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1, 1, 1); // 색상 초기화
        isTracing = true; // 따라가기 초기화
        currentEnemyHP = 10; // TODO: 
    }

    void Update()
    {
        Trace();
    }

    void Trace()
    {
        if (isTracing)
        {
            if (Vector2.Distance(transform.position, target.position) > distance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else // 사정거리 내로 들어왔을 경우 폭발 시작
            {
                isTracing = false;
                StartCoroutine(Ignite());
                StartCoroutine(Explode());
            }
        }
    }
    IEnumerator Explode() // 폭발하는 부분
    {
        yield return new WaitForSeconds(1.5f); // 1.5초 뒤 폭발
        ResourceManager.Instance.Create("explosionEffect", transform.position);
        if (currentEnemyHP > 0) // 오브젝트 풀에 중복으로 들어가는 것 방지.
        {
            ResourceManager.Instance.Deactivate(gameObject);
        }
    }

    IEnumerator Ignite() // 점점 빨게지는 부분
    {
        float colorGB = 1;
        while (true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1, colorGB, colorGB);
            colorGB -= Time.deltaTime * 9;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void Hit(float damage)
    {
        base.Hit(damage);
    }

    public void Scat()
    {
        StartCoroutine(DeleteRigidBody());
    }
    IEnumerator DeleteRigidBody()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(GetComponent<Rigidbody2D>());
    }
}
