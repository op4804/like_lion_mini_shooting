using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHp { get; set; }
    public float currentHp { get; set; }
    
    void Awake()
    {
        maxHp = 100;
        currentHp = maxHp;
    }

    public void Hit(float damage) 
    {
        currentHp -= damage; 
    }

}
