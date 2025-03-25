using NUnit.Framework;
using System.Collections;
using UnityEngine;

public interface IBossPattern
{
    bool isPatternStop { get; set; }
    bool isPatternEnd { get; set; }

    //패턴에 사용할 탄막 오브젝트들
    //패턴에 사용할 소환,무빙좌표 오브젝트들

    IEnumerator StopPattern();//패턴 그 프로그래스코루틴까지 돌고 종료
    IEnumerator PatternProgress(); // 패턴 내용을 진행 하는 코루틴 Move(),Spawn()과 시간 제어를 섞어서 패턴 구현
    IEnumerator Move(Transform pos, float moveSpeed); // 무빙 코루틴 움직일지
    IEnumerator Spawn(GameObject go, Transform pos); // 소환 코루틴
}
