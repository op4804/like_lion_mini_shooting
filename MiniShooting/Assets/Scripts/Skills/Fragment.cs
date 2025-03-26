using UnityEngine;

public class Fragment : Skill
{
    // �� �κ��� �ش� ��ų�� ���� ��ġ�� ���ø�˴ϴ�.
    private int fragmentNum = 3; // ���� ����
    private float fragmentSpeed = 5.0f; // ���� �ӵ�
    private float fragmentDuration = 0.5f; // ���� ���� �ð�
    private float fragmentDamage = 1f;

    //��ų �ʱ�ȭ�Դϴ�.
    public override void InitializeSkill(Player player)
    {
        base.InitializeSkill(player);

        //��ų �����Դϴ�.
        //�̸�, ����, ��ų Ÿ��(true�� active, false�� �нú�), Ȱ��ȭ���δ� �ʼ����Դϴ�.
        //�̸� ,������ ��ų �����ϴµ� �������ʾƼ� �ƹ��ų� ��� ũ�� ��������ϴ�.
        skillName = "����";
        effectKey = "Fragment";
        description = "�Ѿ��� ���� ������ ���� ������ �ֺ��� Ƨ�ϴ�. ";
        skillType = false; // true : ��Ƽ�� ��ų false : �нú� ��ų
        isUnlocked = true;
    }

    //��ų ȿ�� ����Դϴ�.
    //Log�� ������ ��� ��� �ݵ�� �����ؾ��մϴ�.
    public override void ApplyEffect()
    {
        if (!isUnlocked) return; //��ų Ȱ��ȭ ���θ� üũ�ϴ� ���Դϴ�. ��Ȱ��ȭ��(false) ȿ�� ������ �ȵ˴ϴ�.

        Debug.Log("Fragment ��ų �����"); //����� ����ƴ��� ������ ��������Ŷ� �������ŵ� �˴ϴ�.

        SkillManager.Instance.AddBulletModifier((bullet) => //��ų �Ŵ������� �ش� ȿ���� ������ݴϴ�.
        {
            if (!bullet.GetComponent<FragmentBullet>())
            {
                var fragmentingBullet = bullet.AddComponent<FragmentBullet>(); //���� ȿ���� ����ϴ� ����Դϴ�. ȿ�� ���, ȿ�� ������ �����մϴ�.
                                                                               //�ؿ��� ȿ�� ��, ������ ���ø� ���ٵ� �ش� ��ɰ� ������� �۵��մϴ�.

                fragmentingBullet.SetFragmentValue(fragmentNum, fragmentSpeed, fragmentDuration, fragmentDamage); //���� ��ų ��ġ�� �Ѱ��ִ� ����Դϴ�.
                fragmentingBullet.SetEffectKey(effectKey); // Ű ���� �������ݴϴ�.
            }
        });
    }
}
