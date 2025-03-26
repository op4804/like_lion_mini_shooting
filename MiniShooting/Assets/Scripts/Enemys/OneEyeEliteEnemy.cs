using System.Collections;
using UnityEngine;

public class OneEyeEliteEnemy : EliteEnemy
{
    protected override void OnEnable() // 초기화 및 시작
    {
        currentEnemyHP = 500;
        StartCoroutine(MoveFoward());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MoveFoward() // 앞으로 가기
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForFixedUpdate();
            transform.Translate(Vector2.left * Time.deltaTime * 2f);
        }

        StartCoroutine(SpecialAttack()); // 특수 공격 시작
    }

    IEnumerator SpecialAttack()
    {
        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject go = Instantiate(ResourceManager.Instance.oneEyeEliteEnemyBullet, transform.position, Quaternion.identity);

                float angle = 360 / 10 * i;

                go.GetComponent<oneEyeEliteEnemyBullet>().Setdir(new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0));
                go.GetComponent<oneEyeEliteEnemyBullet>().SetRot(angle);
            }


            yield return new WaitForSeconds(5f);


        }
    }
}
