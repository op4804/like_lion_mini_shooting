using System.Collections;
using UnityEngine;

public class SlashWaring : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject slash;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AutoDestroy());
    }

    IEnumerator MakeSlashEffect()
    {
        Instantiate(slash, this.transform.position, this.transform.rotation);
        yield return null;
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(1f); // 1초 기다림
        StartCoroutine(MakeSlashEffect());
        Destroy(gameObject); // 자기 자신 파괴
    }
}
