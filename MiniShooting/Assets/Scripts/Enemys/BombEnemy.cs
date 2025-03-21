using System.Collections;
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
        currentEnemyHP = 10; // TODO: 
    }

    void Update()
    {
        Trace();
    }

    void Trace()
    {
        if(isTracing)
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
        Instantiate(ResourceManager.Instance.explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator Ignite() // 점점 빨게지는 부분
    {
        float colorGB = 1;
        while(true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, colorGB, colorGB);            
            colorGB -= Time.deltaTime * 9;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
