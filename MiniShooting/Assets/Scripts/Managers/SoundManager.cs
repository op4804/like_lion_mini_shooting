using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

// ���� �Ŵ��� ��ũ��Ʈ
// Header 3���� �����ؼ� ��ü �ҽ� ���� �κ�, BGM, ȿ�������� �������ϴ�.

// ���������� SoundManager�� �̱������� ����Ͽ� SoundManager�� ���� �ٲ� �ı����� �ʰ� �����ǰ� ����.
// ���� �ε�� ������ OnSceneLoaded�� ȣ���ϰ� �Ͽ���, �̹� �ν��Ͻ��� �����ϸ� �ߺ� ������Ʈ�� �ı��˴ϴ�.

// ������ �����ϸ� ���� ���� �̸��� ���� ��ϵ� BGM�� ������ ���
// ����, ���ο� ���� �ε�Ǹ� �ش� ���� �̸��� ���� ��ϵ� BGM ���
// ���ѷα� - �Ϲ� �������� - ���� ���������� ������ switch - case ������ �� �̸��� ���� ����� BGM�� ����.
// ���� ��� ���� BGM�� �ٸ� ��쿡�� ���� �� ���.
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // �̱���

    [Header("��� ���� �� ȿ���� �ҽ�")]
    [SerializeField]
    private AudioSource BGMSource;
    [SerializeField]
    private AudioSource SFXSource;

    [Header("��� ����")]
    public AudioClip mainClip; // �� Main BGM
    public AudioClip prologueClip; // �� Prologue BGM
    public AudioClip bossClip; // �� BossTest BGM

    [Header("ȿ����")]
    [SerializeField] private AudioClip attackClip; // player ���� �� ȿ����
    [SerializeField] private AudioClip hitClip; // bullet�� enemy���� �浹�ϸ� ����Ǵ� ȿ����
    [SerializeField] private AudioClip dieClip; // player ��� �� ȿ����
    [SerializeField] private AudioClip upgradeClip; // player ���׷��̵� ȹ�� �� ȿ����

    [Header("���� �÷��̾�� ���� �� �ǰ���")]
    public AudioClip bombEnemyDamageClip;
    public AudioClip wolfEnemyDamageClip;
    public AudioClip OneEyeEnemyDamageClip;
    public AudioClip OneEyeEliteDamageClip;
    public AudioClip OneEyeEliteSpecialClip;

    [Header("��� ����")]
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

    // Boss �� ���� �� ����� ��� �ڷ�ƾ
    private IEnumerator PlayBossWarningThenBGM()
    {
        BGMSource.Stop();

        SFXSource.PlayOneShot(bossClip);
        yield return new WaitForSeconds(bossClip.length);

        PlayBGMForScene("BossTest");
    }

    // ����Ʈ ���� ���� �� �ڷ�ƾ ���
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
                Debug.LogWarning($"�� '{sceneName}'�� ���� BGM�� �������� �ʾҽ��ϴ�.");
                return;
        }

        if (BGMSource.clip != targetClip)
        {
            BGMSource.clip = targetClip;
            BGMSource.loop = true;
            BGMSource.Play();
            Debug.Log($"�� �� '{sceneName}'�� BGM ��� ����: {targetClip.name}");
        }
    }

    // �÷��̾� ��� �κ�
    public void PlayerAttack() => SFXSource.PlayOneShot(attackClip);
    public void PlayerHit() => SFXSource.PlayOneShot(hitClip);
    public void PlayerDie() => SFXSource.PlayOneShot(dieClip);
    public void PlayerUpgrade() => SFXSource.PlayOneShot(upgradeClip);

    // �� �ǰ��� ��� �κ�
    public void BombEnemyAttack() => SFXSource.PlayOneShot(bombEnemyDamageClip);
    public void WolfEnemyAttack() => SFXSource.PlayOneShot(wolfEnemyDamageClip);
    public void OneEyeEnemyAttack() => SFXSource.PlayOneShot(OneEyeEnemyDamageClip);
    public void OneEyeEliteAttack() => SFXSource.PlayOneShot(OneEyeEliteDamageClip);
    public void OneEyeEliteSpecial() => SFXSource.PlayOneShot(OneEyeEliteSpecialClip);

    //// ����� ��� �κ�
    //public void PlayBossWarning() => SFXSource.PlayOneShot(bossClip);
    //public void PlayEliteWarning() => SFXSource.PlayOneShot(EliteWarningClip);

}
