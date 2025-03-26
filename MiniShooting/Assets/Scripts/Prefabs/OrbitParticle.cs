using UnityEngine;

public class OrbitParticle : MonoBehaviour
{
    public Transform pivot;

    // ���� ������
    private float radius = 1f;
    private float angle;
    // ���� �ӵ�
    private float orbitSpeed;
    private float x;
    private float y;
    private float z;

    
    void Update()
    {
        // ���� �ð��� ���� ������ ���
        angle = Time.time * orbitSpeed;

        if(gameObject.name == "LeftDown")LeftDown();
        if(gameObject.name == "RightUp")RightUp();
        if(gameObject.name == "LeftToRight") LeftToRight();

        // ������Ʈ�� ��ġ ����
        transform.position = new Vector3(x, y, z);
    }

    void LeftDown()
    {
        orbitSpeed = 4;
        x = pivot.position.x + radius * Mathf.Cos(angle);
        y = pivot.position.y + radius * Mathf.Cos(angle);
        z = pivot.position.z + radius * Mathf.Sin(angle);
    }

    void RightUp()
    {
        orbitSpeed = 5;
        x = pivot.position.x + radius * Mathf.Cos(angle);
        y = pivot.position.y - radius * Mathf.Cos(angle);
        z = pivot.position.z - radius * Mathf.Sin(angle);
    }

    void LeftToRight()
    {
        orbitSpeed = 6;
        x = pivot.position.x + radius * Mathf.Cos(angle);
        z = pivot.position.z + radius * Mathf.Sin(angle);
    }
}
