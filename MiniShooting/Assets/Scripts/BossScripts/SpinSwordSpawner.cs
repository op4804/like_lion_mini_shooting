using System.Collections;
using UnityEngine;

public class SpinSwordSpawner : MonoBehaviour
{
    public GameObject spawn; // 사라질 때 소환할 오브젝트

    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(1f); // 1초 기다림

        if (spawn != null)
        {
            Instantiate(spawn, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // 자기 자신 파괴
    }
}
