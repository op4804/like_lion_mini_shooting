using Unity.VisualScripting;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [HideInInspector]
    public static GameManger Instance = null;

    private bool isGameOver;

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


    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
