using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;
using System;

public class oneEyeEliteEnemyBullet : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameManager.Instance.player;
    }
    private void OnEnable()
    {
        SoundManager.instance.OneEyeEliteAttack(); // 엘리트 공격 시 효과음 재생
        StartCoroutine(FollowPlayer());
    }   

    internal void Init(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator FollowPlayer() // 앞으로 가기
    {
        for (int i = 0; i < 180; i++)
        {
            yield return new WaitForFixedUpdate();
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime);
        }
        SoundManager.instance.OneEyeEliteSpecial(); // SpecialAttack() 효과음 재생
        StartCoroutine(SpecialAttack());
    }

    IEnumerator SpecialAttack()
    {

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForFixedUpdate();
            transform.Translate(Vector3.left * Time.deltaTime * 2f);
        }

        Destroy(gameObject);
    }


}
