using UnityEngine;
using TMPro;  // ← CHANGED: For TextMeshPro (not UnityEngine.UI)

public class ScoreManager : MonoBehaviour
{
    // Singleton for easy access
    public static ScoreManager Instance;

    // UI Reference
    public TextMeshProUGUI scoreText;  // ← CHANGED: Now uses TextMeshPro

    // Score tracking
    private int score = 0;

    void Awake()
    {
        // Setup singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Update score display at start
        UpdateScoreUI();
    }

    // === ADD GOAL ===
    public void AddGoal()
    {
        score++;
        UpdateScoreUI();
        Debug.Log("⚽ GOAL! Score: " + score);
    }

    // === RESET SCORE ===
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
        Debug.Log("Score reset to 0");
    }

    // === UPDATE UI ===
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}