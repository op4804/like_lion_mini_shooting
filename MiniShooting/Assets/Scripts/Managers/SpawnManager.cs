using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject currentSpawnEnemy1;
    private GameObject currentSpawnEnemy2;

    [SerializeField]
    private float spawnDelay1 = 2.0f;
    private float spawnDelay2 = 5.1f;

    void Start()
    {
        // 몬스터 설정
        currentSpawnEnemy1 = ResourceManager.Instance.enemy1;
        currentSpawnEnemy2 = ResourceManager.Instance.enemy2;
        StartCoroutine(Spawn1());
        StartCoroutine(Spawn2());
    }

    void Update()
    {

    }

    IEnumerator Spawn1()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay1);
            Instantiate(currentSpawnEnemy1, new Vector3(transform.position.x + 8, Random.Range(-2.0f, 2.0f), 0), Quaternion.identity);
        }
    }
    IEnumerator Spawn2()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay2);
            Instantiate(currentSpawnEnemy2, new Vector3(transform.position.x + 8, Random.Range(-2.0f, 2.0f), 0), Quaternion.identity);
        }
    }
}
