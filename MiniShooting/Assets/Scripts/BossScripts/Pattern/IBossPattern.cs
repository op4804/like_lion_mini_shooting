using System.Collections;
using UnityEngine;

public interface IBossPattern
{
    //패턴에 사용할 탄막 오브젝트들
    //패턴에 사용할 소환,무빙좌표 오브젝트들
    //패턴타입 회피 or 공격
    //회피패턴은 패턴시간이 지나면 자동으로 다음패턴으로 넘어가고
    //공격패턴은 패턴이 지속될 동안 보스 hp를 일정수치만큼 까면 패턴이 넘어간다.

    IEnumerator StartPattern(); // 패턴 실행
    //IEnumerator Move(); // 무빙 코루틴
    //IEnumerator Spawn(GameObject go ,Transform pos); // 소환 코루틴

}
