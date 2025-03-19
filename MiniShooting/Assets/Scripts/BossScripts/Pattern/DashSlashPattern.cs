using System.Collections;
using UnityEngine;

public class DashSlashPattern : MonoBehaviour
{
    public LineRenderer warningLine;
    public Transform player;
    public float dashSpeed = 15f;
    public float warningDuration = 1.5f;
    public GameObject slashPrefab;


    void Start()
    {
        
    }


    void Update()
    {
        
    }



    public IEnumerator DashSlashPatternC()
    {
        Vector2 dashDir = (player.position - transform.position).normalized;
        Vector2 targetPos = (Vector2)transform.position + dashDir * 10f;

        warningLine.enabled = true;
        warningLine.SetPosition(0, transform.position);
        warningLine.SetPosition(1, targetPos);
        yield return new WaitForSeconds(warningDuration);
        warningLine.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = dashDir * dashSpeed;
        yield return new WaitForSeconds(0.5f);

        rb.linearVelocity = Vector2.zero;

        GameObject slash = Instantiate(slashPrefab, transform.position, Quaternion.identity);
        LineRenderer slashLine = slash.GetComponent<LineRenderer>();
        slashLine.SetPosition(0, transform.position);
        slashLine.SetPosition(1, targetPos);

        Destroy(slash, 1f);
    }

}
