using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

// �⺻�����δ� �׳� ���� �ٿ��ֱ��ϰ� ǥ���� ������ �����ֽø�˴ϴ�.
// ��ų ������-> ��ų -> ��ų�Ŵ��������� ����˴ϴ�.
// �ش� Ŭ������ ��ų �������Դϴ�.
// ���� �������� ���, ȿ��, ����Ʈ�� ��� ������ �� Ŭ������ ���մϴ�.
// å�� ����, �����̶�� �����Ͻø� �ɰͰ����ϴ�.
// �����ο����� �ʼ������� �����ؾ��ϴ� �Լ��� �����ϴ�.
// ����Ʈ�� �ִٸ� exoplosion�� ����Ʈ �κ��� �����ϼ���.

// �⺻������ bullet�� �پ ���δٰ� �����Ͻð� �����Ͻø�˴ϴ�.
// '**'�� ���� �κ��� �ݵ�� �����ؾ��մϴ�.
// �⺻ �߻�ü�� �����ӻ��� �浹�̳� ��Ÿ Ư���� �����Ͻð� �׳� ���� ��� �۵��ϴ����� �����Ͻø�˴ϴ�.
// ���� ��� �߻�ü�� ������ ������ Ư���� �״�� ������������ ���� �������ϼŵ��ǰ�
// �ε������� �浹ó���� ��ų�Ŵ����� ���ֱ⶧����
// ȿ���� ���̸� �˾Ƽ� �⺻ �߻�ü�� ������ ���õ˴ϴ�.

public class BouncingBullet : MonoBehaviour
{    
    private string effectKey; //**Ű���Դϴ�. �ݵ�� �־���մϴ�.
    private int maxBounces = 3; //�ִ� ƨ��� Ƚ��
    private int currentBounce = 0; //ƨ�� Ƚ�� ī��Ʈ
    private float bounceRadius = 200f; //ƨ��� ����    
    private bool isFirst = false; //ù �浹 ����

    private GameObject currentTarget; // ���� Ÿ�� ����

    private List<GameObject> alreadyHit = new List<GameObject>(); // ������ �ߺ� ����

    private Rigidbody2D rb; // ù�浹 ������ �߻�ü �̵��� ���� ����ϴ� �����Դϴ�.

    // ��ų���� �������� ������ ������ ���� �������ִ� �Լ��Դϴ�.
    public void SetBounceValues(int maxBounces, float bounceRadius)
    {
        this.maxBounces = maxBounces;
        this.bounceRadius = bounceRadius;
    }
        
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>(); // rb �ʱ�ȭ

        //Debug.Log($"{effectKey}ȿ�� ������Դϴ�. ");

        //**Ű���� �������ݴϴ�. �ݵ�� �����ؾ��մϴ�.
        if (!string.IsNullOrEmpty(effectKey))
        {
            //Debug.Log($"{GetInstanceID()}�� {effectKey}ȿ�� ���", this);
            SkillManager.Instance.RegisterBulletEffect(gameObject, effectKey);
        }
    }

    void OnDisable() //������ƮǮ ���� �߻�ü �ʱ�ȭ�ڵ��Դϴ�. �ʿ��Ͻø� �����ø�˴ϴ�.
    {
        currentBounce = 0;
        isFirst = false;
        currentTarget = null;
        alreadyHit.Clear(); // List �ʱ�ȭ
        transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        if (isFirst == true)
        {
            if (currentTarget == null || !currentTarget || currentTarget.GetComponent<Enemy>()?.IsDead() == true)
            {
                SkillManager.Instance.NotifyEffectComplete(gameObject, effectKey); //**destroy ����� �Ѵٰ� �����Ͻø� �˴ϴ�. ������ ��򰡿� �ݵ�� �����Ǿ���մϴ�.
                                                                              //���� destroy�� �ƴϰ� ��ų�Ŵ������� ȿ���� ������ �ȴ� �˷��ִ� �Լ��Դϴ�.
                                                                              //��, ������ ���������� ��ų ȿ���� �������ִ� �Լ���� �����Ͻø�˴ϴ�.
                return;
            }
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * Player.Instance.GetbulletSpeed(); //���� Ÿ������ �̵�

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    //�� �����δ� �ٿ�� �������̴� �� ����ð� �����Ͻø� �˴ϴ�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Boss")) // �浹�� ������ �Ǵ�
        {
            var enemy = collision.gameObject; // �浹 ��ü�� ����

            if (alreadyHit.Contains(enemy)) return; // �̹� ���� ���̶�� ����

            enemy.GetComponent<Enemy>().Hit(Player.Instance.GetAttack()); // ������ ó��

            alreadyHit.Add(enemy); //�̹� ���������� ����
            isFirst = true; //ù �浹 Ȱ��ȭ

            //Debug.Log($"{GetInstanceID()}�� {currentBounce}��° Ÿ�� : {collision}, ID: {collision.GetInstanceID()}", this);
            //Debug.Log($"���� : {currentBounce}, �ִ� : {maxBounces}");

            if (currentBounce >= maxBounces)
            {
                //Debug.Log($"Ÿ�� �Ϸ� ���� �õ�");
                SkillManager.Instance.NotifyEffectComplete(gameObject, effectKey);
            }
            else
            {
                NextTarget(enemy.transform.position);//���� Ÿ�� ã�� �Լ� ȣ��(�浹 ��ü ��ǥ ����)
                currentBounce++;
            }
        }
    }

    private void NextTarget(Vector3 currentPosition)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(currentPosition, bounceRadius); //��ü ���� bounceRadius��ŭ �˻��ؼ� ��ü�� ������
        GameObject nextTarget = null; //���� Ÿ�� ���� ����
        float closestDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy") && !alreadyHit.Contains(hit.gameObject)) //�̹� ���� ��ü �ĺ�, �˻��� ��ü�� ������ �Ǵ�
            {
                Enemy enemy = hit.GetComponent<Enemy>();

                if (enemy != null && !enemy.IsDead())
                {
                    float distance = Vector3.Distance(currentPosition, hit.transform.position); //Ÿ�ٰ��� �Ÿ� ���

                    //���� ���� Ÿ���� ���� ����� Ÿ���� ����
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nextTarget = hit.gameObject;
                    }
                }
            }
        }

        if (nextTarget != null)
        {
            currentTarget = nextTarget;
            //Debug.Log($"{GetInstanceID()}�� ���� ��� : {nextTarget}, ID: {nextTarget.GetInstanceID()}", this);
        }

        else
        {
            Debug.Log($"Ÿ�� ���� ����", this);
            SkillManager.Instance.NotifyEffectComplete(gameObject, effectKey); // ��ȿ�� Ÿ���� ������ �ٷ� ����
        }
    }

    //**Ÿ�ְ̹� ������� Ű���� �����Ǵ� ��� ����ϴ� ����Դϴ�. �ʼ��� �����ؾ��մϴ�.
    public void SetEffectKey(string keyName)
    {
        effectKey = keyName;

        if (SkillManager.Instance != null && gameObject.activeInHierarchy)
        {
            SkillManager.Instance.RegisterBulletEffect(gameObject, effectKey);
            Debug.Log($"[SetEffectKey] {GetInstanceID()}�� {effectKey} ȿ�� ���", gameObject);
        }
    }
}
