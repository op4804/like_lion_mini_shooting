using UnityEngine;

//기본적으로는 그냥 복사 붙여넣기하고 표기한 변수만 고쳐주시면됩니다.
//스킬 구현부-> 스킬 -> 스킬매니저순으로 진행됩니다.
//해당 클래스는 스킬 클래스입니다.
//어떤 스킬인지 설명하는 책의 표지라고 보시면 될것같습니다.

public class Bounce : Skill
{
    //이 부분은 해당 스킬의 고유 수치만 쓰시면됩니다.
    private int maxBounces = 3; //최대 튕기는 횟수
    private float bounceRadius = 100f; //튕기는 범위
    

    //스킬 초기화입니다.
    public override void InitializeSkill(Player player)
    {
        base.InitializeSkill(player);

        //스킬 설정입니다.
        //이름, 설명, 스킬 타입(true면 active, false면 패시브), 활성화여부는 필수값입니다.
        //이름 ,설명은 스킬 실행하는데 사용되진않아서 아무거나 적어도 크게 상관없습니다.
        skillName = "바운스";
        effectKey = "Bounce";
        description = "총알이 적들 사이를 튕깁니다.";
        skillType = false;
        isUnlocked = true;
    }

    //스킬 효과 등록입니다.
    //Log를 제외한 모든 명령 반드시 구현해야합니다.
    public override void ApplyEffect()
    {
        if (!isUnlocked) return; //스킬 활성화 여부를 체크하는 곳입니다. 비활성화시(false) 효과 적용이 안됩니다.

        Debug.Log($"{effectKey} 스킬 적용됨"); //제대로 적용됐는지 보려고 적어놓은거라 안적으셔도 됩니다.

        SkillManager.Instance.AddBulletModifier((bullet) => //스킬 매니저한테 해당 효과를 등록해줍니다.
        {
            if (!bullet.GetComponent<BouncingBullet>())
            {
                var bouncingBullet = bullet.AddComponent<BouncingBullet>(); //실제 효과를 등록하는 명령입니다. 효과 등록, 효과 삭제와 무관합니다.
                                                                            //밑에서 효과 온, 오프로 예시를 들텐데 해당 명령과 상관없이 작동합니다.

                bouncingBullet.SetBounceValues(maxBounces, bounceRadius); //고유 스킬 수치를 넘겨주는 명령입니다.
                bouncingBullet.SetEffectKey(effectKey); // 키 값을 설정해줍니다.
            }
        });
    }
}