using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class ResourceManager : MonoBehaviour
{
    [HideInInspector]
    public static ResourceManager Instance = null;

    [Header("PlayrtBullets")]
    public GameObject playerBullet;

    [Header("Enemies")]
    public GameObject oneEyeEnemy;
    public GameObject wolfEnemy;
    public GameObject bombEnemy;

    [Header("Enemy Bullets")]
    public GameObject enemyBullet;
    public GameObject claw;
    public GameObject oneEyeEliteEnemyBullet;

    [Header("Elite Enemies")]
    public GameObject oneEyeEliteEnemy;
    public GameObject t;
    public GameObject t2;

    [Header("Skills")]
    public GameObject eagle;

    [Header("etc")]
    public GameObject expParticle;
    public GameObject explosionEffect;
    public GameObject frag;

    // 오브젝트 풀링 구현
    private List<GameObject> prefabs = new(); // 모든 프리팹을 가지고 있는 리스트
    private Dictionary<string, Queue<GameObject>> objectDic = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        prefabs.Add(playerBullet);
        prefabs.Add(oneEyeEnemy);
        prefabs.Add(wolfEnemy);
        prefabs.Add(bombEnemy);
        prefabs.Add(enemyBullet);
        prefabs.Add(claw);
        prefabs.Add(oneEyeEliteEnemyBullet);
        prefabs.Add(oneEyeEliteEnemy);
        //
        //
        prefabs.Add(expParticle);
        prefabs.Add(explosionEffect);
        prefabs.Add(eagle);
        prefabs.Add(frag);
    }

    
    public GameObject Create(string key, Vector2 position)
    {
        GameObject go;
        if (objectDic.ContainsKey(key) && objectDic[key].Count > 0) // 해당 게임 오브젝트(프리팹)이 비활성화 된 오브젝트 풀에 있다면~
        {
            go = objectDic[key].Dequeue();
            go.SetActive(true);
            go.transform.position = position;
            return go;
        }
        else 
        {
            Debug.Log(key);
            go = Instantiate(stringToGameobject(key), position, Quaternion.identity);
            return go;
        }        
    }
    
    public GameObject Create(string key, Vector3 position) // vector3를 위한 오버로드
    {
        GameObject go;
        if (objectDic.ContainsKey(key) && objectDic[key].Count > 0) // 해당 게임 오브젝트(프리팹)이 비활성화 된 오브젝트 풀에 있다면~
        {
            go = objectDic[key].Dequeue();
            Debug.Log($"[Pool] 꺼냄: {key}, ID: {go.GetInstanceID()}", go);
            go.SetActive(true);
            go.transform.position = position;
            return go;
        }
        else // 없다면 새로 생성
        {
            Debug.LogWarning($"[Pool] 새로 생성됨 - key: {key}");
            go = Instantiate(stringToGameobject(key), position, Quaternion.identity); 
            return go;
        }        
    }

    public void Deactivate(GameObject gameObject)
    {
        gameObject.SetActive(false);
        string key = gameObject.name.Replace("(Clone)", "");

        Debug.Log($"[pool] 꺼짐 : {key}, ID: {gameObject.GetInstanceID()}", gameObject);

        if (objectDic.ContainsKey(key))
        {
            objectDic[key].Enqueue(gameObject);
        }
        else
        {
            objectDic.Add(key, new());
            objectDic[key].Enqueue(gameObject);
        }
    }
    public void Deactivate(GameObject gameObject, float delay) // 오브젝트 풀링 지연 회수
    {
        StartCoroutine(DeactivateDelay(gameObject, delay));
    }
    IEnumerator DeactivateDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Deactivate(gameObject);
    }

    public GameObject stringToGameobject(string str) // 문자열을 넣으면 해당하는 프리팹을 반환하는 함수
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            if (prefabs[i].name == str)
            {
                return prefabs[i];
            }
        }
        return null;
    }
}
