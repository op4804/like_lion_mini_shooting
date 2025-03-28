using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class BarrierAndSwordPattern : MonoBehaviour, IBossPattern
{
    public GameObject Boss;//��������������������
    public GameObject swordBarrier;// ��ȯ�Ұ�
    public Transform spawnPos1, spawnPos2, spawnPos3, spawnPos4;// ��ȯ ��ġ
    public static int swordCount;


    public float moveSpeed = 10f;// ���� �̵��ӵ�
    public List<Transform> movePoss;//���� �̵� ��ǥ
    public Transform startPos;

    public bool isPatternEnd { get; set; }
    public bool isPatternStop { get; set; }

    void Awake()
    {

    }

    public IEnumerator StopPattern()
    {
        Debug.Log("��������");
        isPatternStop = true;
        yield return null;
    }
    public IEnumerator PatternProgress() // �ڷ�ƾ�� �̿��� �Ѿ� �߻�
    {
        isPatternStop = false;
        yield return StartCoroutine(MoveToStart());

        yield return StartCoroutine(Spawn(swordBarrier, spawnPos1));
        yield return StartCoroutine(Spawn(swordBarrier, spawnPos2));
        yield return StartCoroutine(Spawn(swordBarrier, spawnPos3));
        yield return StartCoroutine(Spawn(swordBarrier, spawnPos4));
        swordCount = 4;
        transform.Find("BossBarrier").gameObject.SetActive(true);//�踮�� Ȱ��ȭ,������ �� ������ �� 4�� ��ȯ
        while (true)
        {
            yield return StartCoroutine(Heal());
            yield return new WaitForSeconds(0.1f);
            if(swordCount <= 0 )
            {
                isPatternStop = true;
            }
            if (isPatternStop == true)
            {
                transform.Find("BossBarrier").gameObject.SetActive(false);//�踮�� ��Ȱ��ȭ
                break;
            }
        }
        isPatternEnd = true;
    }
    
    public IEnumerator Heal()
    {
        Boss.GetComponent<Boss>().currentHp += 10;
        yield return new WaitForSeconds(1f);
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

    public IEnumerator Spawn(GameObject go, Transform pos)
    {
        Instantiate(go, pos.position, Quaternion.identity);
        yield return null;
    }

}
