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

    [Header("Cloud")]
    [SerializeField]
    private bool spawnCloud1 = true;
    [SerializeField]
    private bool spawnCloud2 = true;
    [SerializeField]
    private bool spawnCloud3 = true;
    private float speed;

    void Start()
    {
        StartCoroutine(Spawn1());
        StartCoroutine(Spawn2());
        StartCoroutine(Spawn3());
        StartCoroutine(SpawnElite1());
        StartCoroutine(SpawnElite2());
        StartCoroutine(SpawnElite3());
        StartCoroutine(SpawnCloud1());
        StartCoroutine(SpawnCloud2());
        StartCoroutine(SpawnCloud3());
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
        if (spawnEliteEnemy3Test)
        {
            yield return new WaitForSeconds(spawnDelay3);
            ResourceManager.Instance.Create("bombEliteEnemy", new Vector3(transform.position.x + 8, Random.Range(-2.0f, 2.0f), 0));
        }
    }

    IEnumerator SpawnCloud1()
    {
        while (spawnCloud1)
        {
            yield return new WaitForSeconds(spawnDelay1);
            Vector3 spawnPos = new Vector3(transform.position.x + 8, Random.Range(-4.0f, 4.0f), 0);
            if (IsSpawnAreaClearCloudVer(spawnPos))
            {
                GameObject obj = ResourceManager.Instance.Create("cloud1", spawnPos);
                speed = Random.Range(1f, 10.0f);
                obj.GetComponent<smallCloud>().SetSpeed(speed);
            }
        }
    }
    IEnumerator SpawnCloud2()
    {
        while (spawnCloud2)
        {
            yield return new WaitForSeconds(spawnDelay2);
            Vector3 spawnPos = new Vector3(transform.position.x + 8, Random.Range(-4.0f, 4.0f), 0);

            if (IsSpawnAreaClearCloudVer(spawnPos))
            {
                GameObject obj = ResourceManager.Instance.Create("cloud2", spawnPos);
                speed = Random.Range(1f, 10.0f);
                obj.GetComponent<smallCloud>().SetSpeed(speed);
            }
        }
    }
    IEnumerator SpawnCloud3()
    {
        while (spawnCloud3)
        {
            yield return new WaitForSeconds(spawnDelay3);
            Vector3 spawnPos = new Vector3(transform.position.x + 8, Random.Range(-4.0f, 4.0f), 0);

            if (IsSpawnAreaClearCloudVer(spawnPos))
            {
                GameObject obj = ResourceManager.Instance.Create("cloud3", spawnPos);
                speed = Random.Range(1f, 10.0f);
                obj.GetComponent<smallCloud>().SetSpeed(speed);
            }
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

    private bool IsSpawnAreaClearCloudVer(Vector3 position)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, spawnCheckRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Cloud"))
                return false;
        }

        return true;

    }
}
