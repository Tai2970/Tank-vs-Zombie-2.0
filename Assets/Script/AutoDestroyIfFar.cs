using UnityEngine;

public class AutoDestroyIfFar : MonoBehaviour
{
    [Header("Distance Settings")]
    public Transform player;         // The object to measure distance from (usually the player)
    public float maxDistance = 30f;  // Maximum allowed distance before this object is destroyed

    void Update()
    {
        // If no player is assigned or the object is too far from the player, destroy it
        if (player == null || Vector3.Distance(transform.position, player.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
