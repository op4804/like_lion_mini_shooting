using System.Collections;
using UnityEngine;

public class KnifeCircleSpawner : MonoBehaviour
{
    public int knifeCount;
    public GameObject knifePrefab;
    public float radius;
    void Start()
    {
        StartCoroutine(MakeKnifeCircle());
    }

    IEnumerator MakeKnifeCircle()
    {
        for (int i = 0; i < knifeCount; i++)
        {
            // 원형 배치를 위한 각도 계산
            float angle = (360f / knifeCount) * i;
            Vector2 spawnPosition = (Vector2)transform.position + new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );
            // 칼 생성
            GameObject knife = Instantiate(knifePrefab, spawnPosition, Quaternion.identity);
        }
        yield return null;
    }
    void Des()
    {
        Destroy(gameObject);
    }
}
