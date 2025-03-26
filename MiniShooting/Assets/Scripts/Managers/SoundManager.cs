using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

// 사운드 매니저 스크립트
// Header 3개로 구분해서 전체 소스 관리 부분, BGM, 효과음으로 나눴습니다.

// 순차적으로 SoundManager를 싱글톤으로 등록하여 SoundManager가 씬이 바뀌어도 파괴되지 않고 유지되게 설정.
// 씬이 로드될 때마다 OnSceneLoaded를 호출하게 하였고, 이미 인스턴스에 존재하면 중복 오브젝트는 파괴됩니다.

// 게임이 시작하면 현재 씬의 이름에 맞춰 등록된 BGM을 가져와 재생
// 다음, 새로운 씬이 로드되면 해당 씬의 이름에 맞춰 등록된 BGM 재생
// 프롤로그 - 일반 스테이지 - 보스 스테이지의 구성을 switch - case 문으로 씬 이름에 따라 재생할 BGM을 선택.
// 현재 재생 중인 BGM이 다를 경우에만 변경 및 재생.
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤

    [Header("배경 음악 및 효과음 소스")]
    [SerializeField]
    private AudioSource BGMSource;
    [SerializeField]
    private AudioSource SFXSource;

    [Header("배경 음악")]
    public AudioClip mainClip; // 씬 Main BGM
    public AudioClip prologueClip; // 씬 Prologue BGM
    public AudioClip bossClip; // 씬 BossTest BGM

    [Header("효과음")]
    [SerializeField] private AudioClip attackClip; // player 공격 시 효과음
    [SerializeField] private AudioClip hitClip; // bullet이 enemy에게 충돌하면 재생되는 효과음
    [SerializeField] private AudioClip dieClip; // player 사망 시 효과음
    [SerializeField] private AudioClip upgradeClip; // player 업그레이드 획득 시 효과음

    [Header("적이 플레이어에게 공격 시 피격음")]
    public AudioClip bombEnemyDamageClip;
    public AudioClip wolfEnemyDamageClip;
    public AudioClip OneEyeEnemyDamageClip;
    public AudioClip OneEyeEliteDamageClip;
    public AudioClip OneEyeEliteSpecialClip;

    [Header("경고 사운드")]
    [SerializeField] private AudioClip BosswarningClip;
    [SerializeField] private AudioClip EliteWarningClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGMForScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BossTest")
            StartCoroutine(PlayBossWarningThenBGM());
        else
            PlayBGMForScene(scene.name);
    }

    // Boss 씬 진입 시 경고음 재생 코루틴
    private IEnumerator PlayBossWarningThenBGM()
    {
        BGMSource.Stop();

        SFXSource.PlayOneShot(bossClip);
        yield return new WaitForSeconds(bossClip.length);

        PlayBGMForScene("BossTest");
    }

    // 엘리트 몬스터 등장 시 코루틴 재생
    public void PlayEliteWarningWithBGMControl()
    {
        StartCoroutine(PlayEliteWarningRoutine());
    }

    private IEnumerator PlayEliteWarningRoutine()
    {
        BGMSource.Pause();
        SFXSource.PlayOneShot(EliteWarningClip);
        yield return new WaitForSeconds(EliteWarningClip.length);

        BGMSource.UnPause();
    }

    private void PlayBGMForScene(string sceneName)
    {
        AudioClip targetClip = null;

        switch (sceneName)
        {
            case "Main":
                targetClip = mainClip;
                break;
            case "Prologue":
                targetClip = prologueClip;
                break;
            case "BossTest":
                targetClip = bossClip;
                break;
            default:
                Debug.LogWarning($"씬 '{sceneName}'에 대한 BGM이 설정되지 않았습니다.");
                return;
        }

        if (BGMSource.clip != targetClip)
        {
            BGMSource.clip = targetClip;
            BGMSource.loop = true;
            BGMSource.Play();
            Debug.Log($"▶ 씬 '{sceneName}'의 BGM 재생 시작: {targetClip.name}");
        }
    }

    // 플레이어 재생 부분
    public void PlayerAttack() => SFXSource.PlayOneShot(attackClip);
    public void PlayerHit() => SFXSource.PlayOneShot(hitClip);
    public void PlayerDie() => SFXSource.PlayOneShot(dieClip);
    public void PlayerUpgrade() => SFXSource.PlayOneShot(upgradeClip);

    // 적 피격음 재생 부분
    public void BombEnemyAttack() => SFXSource.PlayOneShot(bombEnemyDamageClip);
    public void WolfEnemyAttack() => SFXSource.PlayOneShot(wolfEnemyDamageClip);
    public void OneEyeEnemyAttack() => SFXSource.PlayOneShot(OneEyeEnemyDamageClip);
    public void OneEyeEliteAttack() => SFXSource.PlayOneShot(OneEyeEliteDamageClip);
    public void OneEyeEliteSpecial() => SFXSource.PlayOneShot(OneEyeEliteSpecialClip);

    //// 경고음 재생 부분
    //public void PlayBossWarning() => SFXSource.PlayOneShot(bossClip);
    //public void PlayEliteWarning() => SFXSource.PlayOneShot(EliteWarningClip);

}
