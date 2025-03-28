using UnityEngine;
using System.Collections;

public class wolfEliteEnemy : EliteEnemy
{

    protected override void OnEnable()
    {
        base.OnEnable();
        maxEnemyHp = 500;
        currentEnemyHP = maxEnemyHp;
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
                float randX = Random.Range(gm.maxBounds.x, gm.minBounds.x);
                float randY = Random.Range(gm.maxBounds.y, gm.minBounds.y);
                GameObject go = Instantiate(ResourceManager.Instance.eliteClaw, new Vector3(randX, randY, 0), Quaternion.identity);

            }

            yield return new WaitForSeconds(5f);

        }
    }
}
