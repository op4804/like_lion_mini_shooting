using System.Collections;
using UnityEngine;

public class LazerWaring : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject lazer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AutoDestroy());
    }

    IEnumerator MakeEffect()
    {
        Instantiate(lazer, this.transform.position, this.transform.rotation);
        yield return null;
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(1f); // 1초 기다림
        StartCoroutine(MakeEffect());
        Destroy(gameObject); // 자기 자신 파괴
    }
}
