using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public Vector3 spawnPosition = new Vector3(-0.019f, -1.223f, 0f);

    private GameObject currentBall;
    private bool isSpawning = false;  // ✅ Prevent multiple spawns

    public static BallManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Only spawn if no ball exists
        if (currentBall == null)
        {
            SpawnBall();
        }
    }

    public void SpawnBall()
    {
        // ✅ Prevent multiple spawns
        if (isSpawning) return;
        isSpawning = true;

        // Clean up old ball
        if (currentBall != null)
        {
            Destroy(currentBall);
            currentBall = null;
        }

        // ✅ Safety check: Make sure ballPrefab is assigned
        if (ballPrefab == null)
        {
            Debug.LogError("❌ Ball Prefab is NOT assigned in BallManager!");
            isSpawning = false;
            return;
        }

        // Spawn new ball
        currentBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        currentBall.tag = "Ball";

        Debug.Log("🏐 New ball spawned at: " + spawnPosition);
        isSpawning = false;
    }

    public GameObject GetBall()
    {
        return currentBall;
    }
}