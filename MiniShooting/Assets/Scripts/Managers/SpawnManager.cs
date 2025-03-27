using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float spawnDelay1 = 2.0f;
    [SerializeField]
    private float spawnDelay2 = 5.1f;
    [SerializeField]
    private float spawnDelay3 = 5.1f;
    [SerializeField]
    private float spawnCheckRadius = 1.5f;

    [SerializeField]
    private bool spawnEnemy1Test = true;
    [SerializeField]
    private bool spawnEnemy2Test = true;
    [SerializeField]
    private bool spawnEnemy3Test = true;

    [Header("Elite")]
    [SerializeField]
    private bool spawnEliteEnemy1Test = true;
    [SerializeField]
    private bool spawnEliteEnemy2Test = true;
    [SerializeField]
    private bool spawnEliteEnemy3Test = true;

    void Start()
    {
        StartCoroutine(Spawn1());
        StartCoroutine(Spawn2());
        StartCoroutine(Spawn3());
        StartCoroutine(SpawnElite1());
        StartCoroutine(SpawnElite2());
        StartCoroutine(SpawnElite3());
    }

    void Update()
    {

    }

    IEnumerator Spawn1()
    {
        while (spawnEnemy1Test)
        {
            yield return new WaitForSeconds(spawnDelay1);

            Vector3 spawnPos = new Vector3(transform.position.x + 8, Random.Range(-2.0f, 2.0f), 0);

            if (IsSpawnAreaClear(spawnPos))
            {
                ResourceManager.Instance.Create("oneEyeEnemy", spawnPos);
            }
        }
    }

    IEnumerator Spawn2()
    {
        while (spawnEnemy2Test)
        {
            yield return new WaitForSeconds(spawnDelay2);
            Vector3 spawnPos = new Vector3(transform.position.x + 8, Random.Range(-2.0f, 2.0f), 0);

            if (IsSpawnAreaClear(spawnPos))
            {
                ResourceManager.Instance.Create("wolfEnemy", spawnPos);
            }
        }
    }

    IEnumerator Spawn3()
    {
        while (spawnEnemy3Test)
        {
            yield return new WaitForSeconds(spawnDelay3);
            Vector3 spawnPos = new Vector3(transform.position.x + 8, Random.Range(-2.0f, 2.0f), 0);

            if (IsSpawnAreaClear(spawnPos))
            {
                ResourceManager.Instance.Create("bombEnemy", spawnPos);
            }
        }
    }

    IEnumerator SpawnElite1()
    {
        if (spawnEliteEnemy1Test)
        {
            yield return new WaitForSeconds(spawnDelay1);
            ResourceManager.Instance.Create("oneEyeEliteEnemy", new Vector3(transform.position.x + 8, 0, 0));
        }

    }
    IEnumerator SpawnElite2()
    {
        if (spawnEliteEnemy2Test)
        {
            yield return new WaitForSeconds(spawnDelay2);
            ResourceManager.Instance.Create("wolfEliteEnemy", new Vector3(transform.position.x + 8, Random.Range(-2.0f, 2.0f), 0));
        }
    }
    IEnumerator SpawnElite3()
    {
        while (spawnEliteEnemy3Test)
        {
            yield return new WaitForSeconds(spawnDelay3);
            ResourceManager.Instance.Create("bombEnemy", new Vector3(transform.position.x + 8, Random.Range(-2.0f, 2.0f), 0));
        }
    }


    private bool IsSpawnAreaClear(Vector3 position)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, spawnCheckRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
                return false;
        }

        return true;

    }
}
