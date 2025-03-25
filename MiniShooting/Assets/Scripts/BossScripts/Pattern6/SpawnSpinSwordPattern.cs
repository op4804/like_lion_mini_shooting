using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class SpawnSpinSwordPattern : MonoBehaviour, IBossPattern
{
    public GameObject knife;// 소환할거
    public List<Transform> movePoss;//칼 소환 좌표

    public float moveSpeed = 10f;// 보스 이동속도
    public Transform startPos;//보스 이동 좌표

    public bool isPatternEnd { get; set; }
    public bool isPatternStop { get; set; }

    void Start()
    {
    }

    public IEnumerator StopPattern()
    {
        isPatternStop = true;
        yield return null;
    }
    public IEnumerator PatternProgress() // 코루틴을 이용한 총알 발사
    {
        isPatternStop = false;
        yield return StartCoroutine(Move(startPos, moveSpeed));
        while (true)
        {
            int randomIndex = Random.Range(0, movePoss.Count);
            Transform randomPos = movePoss[randomIndex]; //랜덤으로 movePoss중 하나를 선택
            yield return StartCoroutine(Spawn(knife, randomPos));
            yield return new WaitForSeconds(0.75f);
            if (isPatternStop == true)
            {
                break;
            }
        }
        isPatternEnd = true;
    }
    public IEnumerator Move(Transform targetPos, float moveSpeed)
    {
        while (Vector2.Distance(transform.position, targetPos.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator Spawn(GameObject go, Transform pos)
    {
        Instantiate(go, pos.position, Quaternion.identity);
        yield return null;
    }

}

