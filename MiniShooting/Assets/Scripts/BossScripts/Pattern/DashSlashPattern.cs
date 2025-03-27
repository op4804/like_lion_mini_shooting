using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class DashSlash : MonoBehaviour, IBossPattern
{
    public GameObject spawner;// 소환할거
    public Transform playerPos;// 플레이어좌표
    public Transform spawnPos;//소환좌표

    public float moveSpeed = 10f;// 보스 이동속도
    public Transform startPos;//보스 시작 좌표
    public float dashSpeed = 100f;// 보스 대쉬 속도

    public bool isPatternEnd { get; set; }
    public bool isPatternStop { get; set; }


    void Awake()
    {
        playerPos = Player.Instance.transform;
    }

    public IEnumerator StopPattern()
    {
        isPatternStop = true;
        yield return null;
    }
    public IEnumerator PatternProgress()
    {
        isPatternStop = false;
        yield return StartCoroutine(MoveToStart());
        Vector2 dirTemp = (playerPos.position - spawnPos.position).normalized;
        Vector2 backPos = (Vector2)playerPos.position + dirTemp * 15f;
        yield return StartCoroutine(Move(-backPos, dashSpeed / 5));
        while (true)
        {
            //yield return StartCoroutine(MoveToStart());//위치 가운데로 초기화

            Vector2 direction = playerPos.position - spawnPos.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle); // 플레이어와 보스 사이 회전값 계산

            Vector2 dir = (playerPos.position - spawnPos.position).normalized;
            Vector2 targetPos = (Vector2)playerPos.position + dir * 15f;


            yield return StartCoroutine(Spawn(spawner, this.transform, rotation));//내위치에 대쉬경고프리팹생성
 
            yield return StartCoroutine(Move(targetPos, dashSpeed));


            if (isPatternStop == true)
            {
                break;
            }
        }
        isPatternEnd = true;
    }

    public IEnumerator MoveToStart()
    {
        while (Vector2.Distance(transform.position, startPos.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, 10 * Time.deltaTime);
            yield return null;
        }
    }
    public IEnumerator Move(Transform targetPos, float moveSpeed)
    {
        while (Vector2.Distance(transform.position, targetPos.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    public IEnumerator Move(Vector2 targetPos, float moveSpeed)
    {
        while (Vector2.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    public IEnumerator Spawn(GameObject go, Transform pos)
    {
        Instantiate(go, pos.position, Quaternion.identity);
        yield return null;
    }
    public IEnumerator Spawn(GameObject go, Transform pos, Quaternion rotation)
    {
        Instantiate(go, pos.position, rotation);
        yield return null;
    }

}

