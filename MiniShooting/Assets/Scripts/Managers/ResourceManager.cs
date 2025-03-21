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

    [Header("etc")]
    public GameObject expParticle;
    public GameObject explosionEffect;

    // ������Ʈ Ǯ�� ����
    private List<GameObject> prefabs = new(); // ��� �������� ������ �ִ� ����Ʈ
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
        prefabs.Add(expParticle);
        prefabs.Add(explosionEffect);
    }

    
    public GameObject Create(string key, Vector2 position)
    {
        GameObject go;
        if (objectDic.ContainsKey(key) && objectDic[key].Count > 0) // �ش� ���� ������Ʈ(������)�� ��Ȱ��ȭ �� ������Ʈ Ǯ�� �ִٸ�~
        {
            go = objectDic[key].Dequeue();
            go.SetActive(true);
            go.transform.position = position;
            return go;
        }
        else // ���ٸ� ���� ����
        {
            go = Instantiate(stringToGameobject(key), position, Quaternion.identity);
            return go;
        }        
    }
    
    public GameObject Create(string key, Vector3 position) // vector3�� ���� �����ε�
    {
        GameObject go;
        if (objectDic.ContainsKey(key) && objectDic[key].Count > 0) // �ش� ���� ������Ʈ(������)�� ��Ȱ��ȭ �� ������Ʈ Ǯ�� �ִٸ�~
        {
            go = objectDic[key].Dequeue();
            go.SetActive(true);
            go.transform.position = position;
            return go;
        }
        else // ���ٸ� ���� ����
        {
            go = Instantiate(stringToGameobject(key), position, Quaternion.identity);
            return go;
        }        
    }

    public void Deactivate(GameObject gameObject)
    {
        gameObject.SetActive(false);
        string key = gameObject.name.Replace("(Clone)", "");
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
    public void Deactivate(GameObject gameObject, float delay) // ������Ʈ Ǯ�� ���� ȸ��
    {
        StartCoroutine(DeactivateDelay(gameObject, delay));
    }
    IEnumerator DeactivateDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Deactivate(gameObject);
    }

    public GameObject stringToGameobject(string str) // ���ڿ��� ������ �ش��ϴ� �������� ��ȯ�ϴ� �Լ�
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
