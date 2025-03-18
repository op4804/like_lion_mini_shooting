using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // @ 수정점. enemy 발사 딜레이 추가
    public float Delay = 20f;

    // @ 수정점. enemybullet 발사 pos 추가
    public Transform Pos;

    // @ 수정점. 발사되는 enemybullet
    public GameObject EnemyBullet;
    void Start()
    {
        // @ 수정점. Invoke()로 함수 시작 시간을 지연
        Invoke("CreateBullet", Delay);
    }

    // @ 수정점. 총알 발사 메서드 CreateBullet()
    void CreateBullet()
    {
        Instantiate(EnemyBullet, Pos.position, Quaternion.identity);

        //재귀 호출 영역
        Invoke("CreateBullet", Delay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
    }

    public void Hit()
    {
        GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
        expParticle.GetComponent<ExperienceParticle>().SetExpAmount(10f);
        Destroy(gameObject);
    }
}
