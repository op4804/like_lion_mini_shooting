using System.Collections.Generic;
using UnityEngine;

// 기본적으로는 그냥 복사 붙여넣기하고 표기한 변수만 고쳐주시면됩니다.
// 스킬 구현부-> 스킬 -> 스킬매니저순으로 진행됩니다.
// 해당 클래스는 스킬 구현부입니다.
// 실제 물리적인 계산, 효과, 이펙트등 모든 구현이 이 클래스에 속합니다.
// 책의 속지, 내용이라고 생각하시면 될것같습니다.
// 구현부에서는 필수적으로 구현해야하는 함수는 없습니다.
// 이펙트가 있다면 exoplosion의 이펙트 부분을 참고하세요.

// 기본적으로 bullet에 붙어서 쓰인다고 생각하시고 구현하시면됩니다.
// 기본 발사체의 움직임빼고 충돌이나 기타 특성은 무시하시고 그냥 실제 어떻게 작동하는지만 구현하시면됩니다.
// 예를 들어 발사체의 앞으로 나가는 특성은 그대로 가지고있으니 따로 구현안하셔도되고
// 부딪혔을때 충돌처리도 스킬매니저가 해주기때문에
// 효과만 붙이면 알아서 기본 발사체의 로직은 무시됩니다.

public class FragmentBullet : MonoBehaviour
{
    private string effectKey;

    // 이 부분은 해당 스킬의 고유 수치만 쓰시면됩니다.
    private int fragmentNum = 3; // 파편 갯수
    private float fragmentSpeed = 5.0f; // 파편 속도
    private float fragmentDuration = 0.5f; // 파편 지속 시간
    private float fragmentDamage = 1f; // 파편 데미지


    // 스킬부의 고유값을 가져와 구현을 위해 세팅해주는 함수입니다.
    public void SetFragmentValue(int fragmentNum, float fragmentSpeed, float fragmentDuration, float fragmentDamage)
    {
        this.fragmentNum = fragmentNum;
        this.fragmentSpeed = fragmentSpeed;
        this.fragmentDuration = fragmentDuration;
        this.fragmentDamage = fragmentDamage;
    }
    void OnEnable() // 오브젝트풀 관련 발사체 초기화코드입니다. 필요하시면 넣으시면됩니다.
    {

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // 충돌이 적인지 판단
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

            // 발사체 이동 방향 (각도)
            float angle = intervalAngle * i;
            // 발사체 이동 방향 (벡터)
            //Cos(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            //sin(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
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


