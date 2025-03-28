using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    IEnumerator spawn1Ro;
    IEnumerator spawn2Ro;
    IEnumerator spawn3Ro;

    private Coroutine spawn1Co;
    private Coroutine spawn2Co;
    private Coroutine spawn3Co;

    private float patern1Delay = 10f;
    private float patern2Delay = 10f;
    private float patern3Delay = 10f;

    private float nextPaternDelay = 5f;

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
        spawn1Ro = Spawn1();
        spawn2Ro = Spawn2();
        spawn2Ro = Spawn3();

        StartCoroutine(SpawnCloud1());
        StartCoroutine(SpawnCloud2());
        StartCoroutine(SpawnCloud3());

        StartCoroutine(SpawnPattern1());
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

    IEnumerator SpawnElite1(System.Action<GameObject> onSpawned = null)
    {
        if (spawnEliteEnemy1Test)
        {
            yield return new WaitForSeconds(spawnDelay1);
            Vector3 pos = new Vector3(transform.position.x + 8, 0, 0);
            GameObject elite = ResourceManager.Instance.Create("oneEyeEliteEnemy", pos);

            onSpawned?.Invoke(elite);
        }

    }

    IEnumerator SpawnElite2(System.Action<GameObject> onSpawned = null)
    {
        if (spawnEliteEnemy2Test)
        {
            yield return new WaitForSeconds(spawnDelay2);

            Vector3 pos = new Vector3(transform.position.x + 8, 0, 0);
            GameObject elite = ResourceManager.Instance.Create("wolfEliteEnemy", pos);

            onSpawned?.Invoke(elite);
        }
    }

    IEnumerator SpawnElite3(System.Action<GameObject> onSpawned = null)
    {
        if (spawnEliteEnemy3Test)
        {
            yield return new WaitForSeconds(spawnDelay3);

            Vector3 pos = new Vector3(transform.position.x + 8, 0, 0);
            GameObject elite = ResourceManager.Instance.Create("bombEliteEnemy", pos);

            onSpawned?.Invoke(elite);
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

    //소환 패턴1
    IEnumerator SpawnPattern1()
    {
        //소환 패턴
        spawn1Co = StartCoroutine(Spawn1());
        yield return new WaitForSeconds(patern1Delay);
        StopCoroutine(spawn1Co);

        //패턴 후 엘리트 등장
        GameObject eliteEnemy = null;
        yield return StartCoroutine(SpawnElite1(obj => eliteEnemy = obj));

        if (eliteEnemy != null)
        {
            Enemy enemy = eliteEnemy.GetComponent<Enemy>();
            yield return new WaitUntil(() => enemy.IsDead());
        }

        //엘리트 죽으면 다음 패턴
        Debug.Log("Patern1 Done");
        yield return new WaitForSeconds(nextPaternDelay);
        yield return StartCoroutine(SpawnPattern2());
    }

    //소환 패턴2
    IEnumerator SpawnPattern2()
    {
        //소환 패턴
        spawn1Co = StartCoroutine(Spawn1());
        spawn2Co = StartCoroutine(Spawn2());

        yield return new WaitForSeconds(patern2Delay);

        StopCoroutine(spawn1Co);

        //패턴 후 엘리트 등장
        GameObject eliteEnemy = null;
        yield return StartCoroutine(SpawnElite2(obj => eliteEnemy = obj));

        if (eliteEnemy != null)
        {
            StopCoroutine(spawn2Co);
            Enemy enemy = eliteEnemy.GetComponent<Enemy>();
            yield return new WaitUntil(() => enemy.IsDead());
        }

        //엘리트 죽으면 다음 패턴
        Debug.Log("Patern2 Done");
        yield return new WaitForSeconds(nextPaternDelay);
        yield return StartCoroutine(SpawnPattern3());
    }

    //소환 패턴3
    IEnumerator SpawnPattern3()
    {
        //소환 패턴
        spawn1Co = StartCoroutine(Spawn1());
        spawn2Co = StartCoroutine(Spawn2());
        spawn3Co = StartCoroutine(Spawn3());

        yield return new WaitForSeconds(patern3Delay);

        StopCoroutine(spawn1Co);
        StopCoroutine(spawn2Co);
        StopCoroutine(spawn3Co);

        //패턴 후 엘리트 등장
        GameObject eliteEnemy = null;
        yield return StartCoroutine(SpawnElite3(obj => eliteEnemy = obj));

        if (eliteEnemy != null)
        {
            Enemy enemy = eliteEnemy.GetComponent<Enemy>();
            yield return new WaitUntil(() => enemy.IsDead());
        }

        //엘리트 죽으면 다음 패턴
        Debug.Log("Patern3 Done");
        yield return new WaitForSeconds(nextPaternDelay);
        yield return BossSequence();
    }
    IEnumerator BossSequence()
    {
        Debug.Log("BOSS Appearance");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Bosstest");
        yield break;
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
