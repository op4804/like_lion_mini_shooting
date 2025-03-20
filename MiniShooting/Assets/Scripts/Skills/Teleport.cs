using UnityEngine;

public class Teleport : Skill
{
    private float teleportDistance;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    public override void InitializeSkill(Player player)
    {
        base.InitializeSkill(player);

        //스킬 설정
        skillName = "텔레포트";
        coolTime = 3.0f; //쿨타임
        teleportDistance = 2f; //이동 거리
        description = "현재 위치에서 다른 위치로 이동합니다.";
        skillType = true;

        Camera mainCamera = Camera.main;
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBounds = new Vector2(topRight.x, topRight.y);
    }

    public override void UseSkill()
    {
        if (!CanUse()) //쿨타임
        {
            Debug.Log($"CoolTime : {Mathf.Max(0, coolTime - (Time.time - lastUsedTime))}");
            return;
        }

        Debug.Log($"{skillName} 스킬 사용");

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) direction.y += 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) direction.y -= 1;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) direction.x -= 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) direction.x += 1;

        if (direction == Vector3.zero) return;

        direction.Normalize();
        Vector3 newPosition = player.transform.position + direction * teleportDistance;

        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        player.transform.position = newPosition;

        SkillCoolTime();
    }
}