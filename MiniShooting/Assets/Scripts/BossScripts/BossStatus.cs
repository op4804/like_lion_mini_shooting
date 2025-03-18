using UnityEngine;

public class BossStatus : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 100;
    [SerializeField]
    private float currentHp;
    
    void Start()
    {
        currentHp = maxHp;
    }

    public float getMaxHp() { return maxHp; }
    public float getCurrentHp() { return currentHp; }
    public void Damaged(int damage) { currentHp -= damage; }

}
