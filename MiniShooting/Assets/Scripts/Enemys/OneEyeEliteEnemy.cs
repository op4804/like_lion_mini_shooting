using System.Collections;
using UnityEngine;

public class OneEyeEliteEnemy : EliteEnemy
{
    protected override void OnEnable()
    {
        currentEnemyHP = 5;
        StartCoroutine(MoveFoward()); // 앞으로 가기
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveFoward()
    {
        for(int i = 0; i < 100; i++)
        {
            yield return new WaitForFixedUpdate();
            transform.Translate(Vector2.left * Time.deltaTime * 2f);
        }

        StartCoroutine(SpecialAttack());
    }

    IEnumerator SpecialAttack()
    {
        yield return new WaitForSeconds(5f);
        // Instantiate();
    }
}
