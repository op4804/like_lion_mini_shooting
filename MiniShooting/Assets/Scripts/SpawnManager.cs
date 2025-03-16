using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{


    private GameObject currentSpawnEnemy;
    void Start()
    {
        // 몬스터 설정
        currentSpawnEnemy = ResourceManager.Instance.enemy1;


        StartCoroutine(Spawn());
    }

    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(currentSpawnEnemy, new Vector3(transform.position.x, Random.Range(-2.0f, 2.0f), 0), Quaternion.identity);
        }
    }
}
