using System.Collections.Generic;
using UnityEngine;

// �⺻�����δ� �׳� ���� �ٿ��ֱ��ϰ� ǥ���� ������ �����ֽø�˴ϴ�.
// ��ų ������-> ��ų -> ��ų�Ŵ��������� ����˴ϴ�.
// �ش� Ŭ������ ��ų �������Դϴ�.
// ���� �������� ���, ȿ��, ����Ʈ�� ��� ������ �� Ŭ������ ���մϴ�.
// å�� ����, �����̶�� �����Ͻø� �ɰͰ����ϴ�.
// �����ο����� �ʼ������� �����ؾ��ϴ� �Լ��� �����ϴ�.
// ����Ʈ�� �ִٸ� exoplosion�� ����Ʈ �κ��� �����ϼ���.

// �⺻������ bullet�� �پ ���δٰ� �����Ͻð� �����Ͻø�˴ϴ�.
// �⺻ �߻�ü�� �����ӻ��� �浹�̳� ��Ÿ Ư���� �����Ͻð� �׳� ���� ��� �۵��ϴ����� �����Ͻø�˴ϴ�.
// ���� ��� �߻�ü�� ������ ������ Ư���� �״�� ������������ ���� �������ϼŵ��ǰ�
// �ε������� �浹ó���� ��ų�Ŵ����� ���ֱ⶧����
// ȿ���� ���̸� �˾Ƽ� �⺻ �߻�ü�� ������ ���õ˴ϴ�.

public class FragmentBullet : MonoBehaviour
{
    private string effectKey;

    // �� �κ��� �ش� ��ų�� ���� ��ġ�� ���ø�˴ϴ�.
    private int fragmentNum = 3; // ���� ����
    private float fragmentSpeed = 5.0f; // ���� �ӵ�
    private float fragmentDuration = 0.5f; // ���� ���� �ð�
    private float fragmentDamage = 1f; // ���� ������


    // ��ų���� �������� ������ ������ ���� �������ִ� �Լ��Դϴ�.
    public void SetFragmentValue(int fragmentNum, float fragmentSpeed, float fragmentDuration, float fragmentDamage)
    {
        this.fragmentNum = fragmentNum;
        this.fragmentSpeed = fragmentSpeed;
        this.fragmentDuration = fragmentDuration;
        this.fragmentDamage = fragmentDamage;
    }
    void OnEnable() // ������ƮǮ ���� �߻�ü �ʱ�ȭ�ڵ��Դϴ�. �ʿ��Ͻø� �����ø�˴ϴ�.
    {

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // �浹�� ������ �Ǵ�
        {
            Fragmenting();
        }
    }

    void Fragmenting()
    {
        float intervalAngle = 360 / fragmentNum;
        for (int i = 0; i < fragmentNum; i++)
        {
            GameObject fragment = Instantiate(ResourceManager.Instance.frag, transform.position, Quaternion.identity);
                //ResourceManager.Instance.Create("frag", transform.position);
            fragment.GetComponent<Frag>().SetDefaultValue(fragmentSpeed, fragmentDuration, fragmentDamage);

            // �߻�ü �̵� ���� (����)
            float angle = intervalAngle * i;
            // �߻�ü �̵� ���� (����)
            //Cos(����)���� ������ ���� ǥ���� ���� pi/180�� ����
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            //sin(����)���� ������ ���� ǥ���� ���� pi/180�� ����
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);

            fragment.GetComponent<Frag>().SetDir(new Vector2(x, y));
        }
        SkillManager.Instance.NotifyEffectComplete(gameObject, name);
    }

    public void SetEffectKey(string keyName)
    {
        effectKey = keyName;

        if (SkillManager.Instance != null && gameObject.activeInHierarchy)
        {
            SkillManager.Instance.RegisterBulletEffect(gameObject, effectKey);
        }
    }
}


