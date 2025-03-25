using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class KnifeEnergyCirclePattern : MonoBehaviour, IBossPattern
{
    public GameObject knife;// ��ȯ�Ұ�
    public Transform spawnPos1;// ��ȯ ��ġ

    public float moveSpeed = 10f;// ���� �̵��ӵ�
    public Transform midPos;//���� �̵� ��ǥ

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
    public IEnumerator PatternProgress() // �ڷ�ƾ�� �̿��� �Ѿ� �߻�
    {
        isPatternStop = false;
        yield return StartCoroutine(Move(midPos, moveSpeed));
        while (true)
        {
            StartCoroutine(Spawn(knife, spawnPos1));
            yield return new WaitForSeconds(0.05f);
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

    public float knifeSpeed = 5f; // Į�� ���ư��� �ӵ�

    public IEnumerator Spawn(GameObject go, Transform pos)
    {
        GameObject obj = Instantiate(go, pos.position, Quaternion.identity);

        // ���� ���� ���� (���� ����)
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        obj.transform.right = randomDir; // �� �ٽ�! ȸ�� ���� ����

        // Rigidbody2D�� �ִٸ� ���� ��
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = randomDir * knifeSpeed;
        }

        yield return null;
    }

}
