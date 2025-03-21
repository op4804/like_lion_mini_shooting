using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject currentSpawnEnemy1;
    private GameObject currentSpawnEnemy2;
    private GameObject currentSpawnEnemy3;

    [SerializeField]
    private float spawnDelay1 = 2.0f;
    [SerializeField]
    private float spawnDelay2 = 5.1f;
    [SerializeField]
    private float spawnDelay3 = 5.1f;

    [SerializeField]
    private bool spawnEnemy1Test = true;
    [SerializeField]
    private bool spawnEnemy2Test = true;   
    [SerializeField]
    private bool spawnEnemy3Test = true;

    void Start()
    {
        // 몬스터 설정
        currentSpawnEnemy1 = ResourceManager.Instance.oneEyeEnemy;
        currentSpawnEnemy2 = ResourceManager.Instance.wolfEnemy;
        currentSpawnEnemy3 = ResourceManager.Instance.bombEnemy;
        StartCoroutine(Spawn1());
        StartCoroutine(Spawn2());
        StartCoroutine(Spawn3());
    }

    void Update()
    {

    }

    IEnumerator Spawn1()
    {
        while (spawnEnemy1Test)
        {
            yield return new WaitForSeconds(spawnDelay1);
            ResourceManager.Instance.Create("oneEyeEnemy", new Vector3(transform.position.x + 8, Random.Range(-2.0f, 2.0f), 0));
        }
    }
    IEnumerator Spawn2()
    {
        while (spawnEnemy2Test)
        {
            yield return new WaitForSeconds(spawnDelay2);
            ResourceManager.Instance.Create("wolfEnemy", new Vector3(transform.position.x + 8, Random.Range(-2.0f, 2.0f), 0));
        }
    }
    IEnumerator Spawn3()
    {
        while (spawnEnemy3Test)
        {
            yield return new WaitForSeconds(spawnDelay3);
            ResourceManager.Instance.Create("bombEnemy", new Vector3(transform.position.x + 8, Random.Range(-2.0f, 2.0f), 0));
        }
    }
}
