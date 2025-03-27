using System;
using System.Collections;
using UnityEngine;

public class SpinSword : Knife
{
    public float rotationSpeed = 1800f; // 회전속도
    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }
    void Update()
    {
        // Z축을 기준으로 회전 (2D라면)
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(1f); // 1초 기다림

        Destroy(gameObject); // 자기 자신 파괴
    }

}
