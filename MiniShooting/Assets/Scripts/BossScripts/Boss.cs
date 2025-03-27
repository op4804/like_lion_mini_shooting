using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHp;
    public float currentHp;
    
    void Awake()
    {
        currentHp = maxHp;
    }

    public void Hit(float damage) 
    {
        currentHp -= damage; 
    }

}
