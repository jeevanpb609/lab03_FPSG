using UnityEngine;

public class GoalkeeperAI : MonoBehaviour
{
    // Movement settings
    public float moveSpeed = 2f;

    // Goal boundaries (left and right posts)
    // Your goal line is at X = -0.03, scale Y = 2.85
    // This means it extends from -0.03 - 1.425 to -0.03 + 1.425
    // = -1.455 to 1.395
    public float minX = -1.455f;  // Left post position
    public float maxX = 1.395f;   // Right post position

    // AI behavior
    private float targetX;
    private float changeTimer;

    void Start()
    {
        // Start at center of goal
        targetX = 0f;
        changeTimer = Random.Range(1f, 3f);
    }

    void Update()
    {
        // Change direction randomly every few seconds
        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0f)
        {
            // Pick a random position between the posts
            targetX = Random.Range(minX, maxX);
            changeTimer = Random.Range(1f, 3f);
        }

        // Move goalkeeper left/right (along X-axis)
        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, targetX, moveSpeed * Time.deltaTime);
        transform.position = pos;
    }

    // Visualize the movement range in the Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 pos = transform.position;
        // Draw left boundary
        Vector3 left = new Vector3(minX, pos.y, pos.z);
        Vector3 right = new Vector3(maxX, pos.y, pos.z);
        Gizmos.DrawLine(left, right);
        // Draw markers at boundaries
        Gizmos.DrawWireSphere(left, 0.1f);
        Gizmos.DrawWireSphere(right, 0.1f);
    }
}