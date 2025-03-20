using UnityEngine;

public class BombEnemy : Enemy
{
    void Start()
    {
        currentEnemyHP = 10; // TODO: 
    }

    void Update()
    {
        Trace();
    }

    void Trace()
    {
        // transform.position = GameManager.Instance.player.transform.position;
    }
}
