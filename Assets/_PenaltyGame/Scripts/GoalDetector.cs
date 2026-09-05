using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("⚽ GOAL! ⚽");

            // ✅ Add score
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddGoal();
            }

            // Destroy the ball
            Destroy(other.gameObject);

            // Spawn a new ball after 1 second
            Invoke(nameof(SpawnNewBall), 1f);
        }
    }

    void SpawnNewBall()
    {
        if (BallManager.Instance != null)
        {
            BallManager.Instance.SpawnBall();
        }
        else
        {
            Debug.LogWarning("⚠️ BallManager not found!");
        }
    }
}