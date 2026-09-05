using UnityEngine;
using System.Collections;

public class GoalDetector : MonoBehaviour
{
    private bool isGoalScored = false;  // Prevent double scoring

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !isGoalScored)
        {
            isGoalScored = true;

            // ✅ SCORE IMMEDIATELY!
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddGoal();
            }

            Debug.Log("⚽ GOAL! Score increased. Ball will stay for 2 seconds...");

            // Start coroutine to destroy ball after 2 seconds
            StartCoroutine(DelayDestroyBall(other.gameObject));
        }
    }

    IEnumerator DelayDestroyBall(GameObject ball)
    {
        // Wait 2 seconds (ball will physically stay inside goal thanks to BackWall collider)
        yield return new WaitForSeconds(1f);

        // Destroy the ball
        Destroy(ball);
        Debug.Log("Ball destroyed after 2 seconds.");

        // Respawn a new ball
        if (BallManager.Instance != null)
        {
            BallManager.Instance.SpawnBall();
        }

        // Reset flag for next goal
        isGoalScored = false;
    }
}