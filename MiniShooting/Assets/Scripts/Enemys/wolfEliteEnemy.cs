using UnityEngine;
using System.Collections;

public class wolfEliteEnemy : EliteEnemy
{
    protected override void OnEnable()
    {
        currentEnemyHP = 500;
        StartCoroutine(MoveFoward()); // 앞으로 가기
    }

    IEnumerator MoveFoward()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForFixedUpdate();
            transform.Translate(Vector2.left * Time.deltaTime * 2f);
        }

        StartCoroutine(SpecialAttack());
    }

    IEnumerator SpecialAttack()
    {
        while (true)
        {
            int clawNum = Random.Range(2, 5);
            for (int i = 0; i < clawNum; i++)
            {
                GameObject go = Instantiate(ResourceManager.Instance.oneEyeEliteEnemyBullet, transform.position, Quaternion.identity);

                float angle = 360 / 10 * i;
                go.GetComponent<oneEyeEliteEnemyBullet>().Init(angle);
            }

            yield return new WaitForSeconds(5f);

        }
    }
}
