using UnityEngine;

//�⺻�����δ� �׳� ���� �ٿ��ֱ��ϰ� ǥ���� ������ �����ֽø�˴ϴ�.
//��ų ������-> ��ų -> ��ų�Ŵ��������� ����˴ϴ�.
//�ش� Ŭ������ ��ų Ŭ�����Դϴ�.
//� ��ų���� �����ϴ� å�� ǥ����� ���ø� �ɰͰ����ϴ�.

public class Bounce : Skill
{
    //�� �κ��� �ش� ��ų�� ���� ��ġ�� ���ø�˴ϴ�.
    private int maxBounces = 3; //�ִ� ƨ��� Ƚ��
    private float bounceRadius = 100f; //ƨ��� ����
    

    //��ų �ʱ�ȭ�Դϴ�.
    public override void InitializeSkill(Player player)
    {
        base.InitializeSkill(player);

        //��ų �����Դϴ�.
        //�̸�, ����, ��ų Ÿ��(true�� active, false�� �нú�), Ȱ��ȭ���δ� �ʼ����Դϴ�.
        //�̸� ,������ ��ų �����ϴµ� �������ʾƼ� �ƹ��ų� ��� ũ�� ��������ϴ�.
        skillName = "�ٿ";
        effectKey = "Bounce";
        description = "�Ѿ��� ���� ���̸� ƨ��ϴ�.";
        skillType = false;
        isUnlocked = true;
    }

    //��ų ȿ�� ����Դϴ�.
    //Log�� ������ ��� ��� �ݵ�� �����ؾ��մϴ�.
    public override void ApplyEffect()
    {
        if (!isUnlocked) return; //��ų Ȱ��ȭ ���θ� üũ�ϴ� ���Դϴ�. ��Ȱ��ȭ��(false) ȿ�� ������ �ȵ˴ϴ�.

        Debug.Log($"{effectKey} ��ų �����"); //����� ����ƴ��� ������ ��������Ŷ� �������ŵ� �˴ϴ�.

        SkillManager.Instance.AddBulletModifier((bullet) => //��ų �Ŵ������� �ش� ȿ���� ������ݴϴ�.
        {
            if (!bullet.GetComponent<BouncingBullet>())
            {
                var bouncingBullet = bullet.AddComponent<BouncingBullet>(); //���� ȿ���� ����ϴ� ����Դϴ�. ȿ�� ���, ȿ�� ������ �����մϴ�.
                                                                            //�ؿ��� ȿ�� ��, ������ ���ø� ���ٵ� �ش� ��ɰ� ������� �۵��մϴ�.

                bouncingBullet.SetBounceValues(maxBounces, bounceRadius); //���� ��ų ��ġ�� �Ѱ��ִ� ����Դϴ�.
                bouncingBullet.SetEffectKey(effectKey); // Ű ���� �������ݴϴ�.
            }
        });
    }
}