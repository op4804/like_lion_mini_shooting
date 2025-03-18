using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [HideInInspector]
    public static ResourceManager Instance = null;

    [Header("PlayrtBullets")]
    public GameObject playerBullet1;

    [Header("Enemies")]
    public GameObject enemy1;
    [Header("Enemy Bullets")]
    public GameObject enemybullet1;

    [Header("etc")]
    public GameObject expParticle;

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
}
