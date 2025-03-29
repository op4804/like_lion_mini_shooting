using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class SpawnSpinSwordPattern : MonoBehaviour, IBossPattern
{
    public GameObject spawner;// 소환할거
    public List<Transform> spawnPoss;//칼 소환 좌표


    public float moveSpeed = 10f;// 보스 이동속도
    public Transform startPos;//보스 이동 좌표

    public bool isPatternEnd { get; set; }
    public bool isPatternStop { get; set; }


    public IEnumerator StopPattern()
    {
        isPatternStop = true;
        yield return null;
    }
    public IEnumerator PatternProgress() // 코루틴을 이용한 총알 발사
    {
        isPatternEnd = false;
        isPatternStop = false;
        yield return StartCoroutine(MoveToStart());
        while (true)
        {
            int randomIndex = Random.Range(0, spawnPoss.Count);
            Transform transform = spawnPoss[randomIndex]; //랜덤으로 spawnPoss중 하나를 선택

            Vector2 randomOffset = Random.insideUnitCircle * 1;
            Vector2 randomPos  = (Vector2)transform.position + randomOffset ;//주변반경 1안의 위치로 조정

            yield return StartCoroutine(Spawn(spawner, randomPos));
            yield return new WaitForSeconds(0.5f);
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

    public IEnumerator Spawn(GameObject go, Vector2 pos)
    {
        Instantiate(go, pos, Quaternion.identity);
        yield return null;
    }
    public IEnumerator Spawn(GameObject go, Transform pos)
    {
        Instantiate(go, pos.position, Quaternion.identity);
        yield return null;
    }
}

