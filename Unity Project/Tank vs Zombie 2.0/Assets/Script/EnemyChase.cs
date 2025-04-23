using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform target;

    void Start()
    {
        FindNearestPlayer();
    }

    void Update()
    {
        if (target == null)
        {
            FindNearestPlayer();
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = player.transform;
            }
        }

        if (closestTarget != null)
        {
            target = closestTarget;
        }
    }
}
