using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement Settings
    public float moveSpeed = 4f;

    // ✅ Pitch Boundaries (calculated from your pitch)
    public float minX = -8.5f;
    public float maxX = 8.5f;
    public float minY = -5.47f;
    public float maxY = 3.53f;

    // Shooting Settings
    public float shootPower = 12f;

    // Private variables
    private Vector3 moveDirection;
    private GameObject currentBall;
    private bool hasBall = false;
    private Rigidbody2D ballRb;

    void Update()
    {
        // === EXIT GAME (ESC Key) ===
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }

        // === PLAYER MOVEMENT (Arrow Keys) ===
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, moveY, 0).normalized;
        Vector3 newPos = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // ✅ Clamp player within pitch boundaries
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        transform.position = newPos;

        // === BALL ATTACHMENT ===
        if (hasBall && currentBall != null)
        {
            Vector3 ballOffset = new Vector3(0, 0.4f, 0);
            currentBall.transform.position = transform.position + ballOffset;
        }
        else if (currentBall == null)
        {
            hasBall = false;
        }

        // === SPACEBAR: Attach or Shoot ===
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!hasBall && currentBall != null)
            {
                AttachBall();
            }
            else if (hasBall && currentBall != null)
            {
                ShootBall();
            }
        }

        // === FIND BALL IF MISSING ===
        if (!hasBall && currentBall == null)
        {
            FindBall();
        }
    }

    // === ATTACH BALL TO PLAYER ===
    void AttachBall()
    {
        if (currentBall == null) return;

        ballRb = currentBall.GetComponent<Rigidbody2D>();
        if (ballRb == null)
        {
            Debug.LogError("Ball has no Rigidbody2D!");
            return;
        }

        ballRb.linearVelocity = Vector2.zero;
        ballRb.angularVelocity = 0f;
        ballRb.bodyType = RigidbodyType2D.Kinematic;

        Vector3 ballOffset = new Vector3(0, 0.4f, 0);
        currentBall.transform.position = transform.position + ballOffset;

        hasBall = true;
        Debug.Log("⚽ Ball ATTACHED! Press Space again to SHOOT!");
    }

    // === SHOOT THE BALL ===
    void ShootBall()
    {
        if (ballRb == null)
        {
            Debug.LogError("Ball Rigidbody2D is missing!");
            return;
        }

        ballRb.bodyType = RigidbodyType2D.Dynamic;
        ballRb.linearVelocity = Vector3.up * shootPower;

        Debug.Log("⚽ SHOT! Power: " + shootPower);

        hasBall = false;
        currentBall = null;
        ballRb = null;
    }

    // === FIND THE BALL ===
    void FindBall()
    {
        if (BallManager.Instance != null)
        {
            currentBall = BallManager.Instance.GetBall();
            if (currentBall != null)
            {
                ballRb = currentBall.GetComponent<Rigidbody2D>();
                hasBall = false;
                return;
            }
        }

        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            currentBall = ball;
            ballRb = currentBall.GetComponent<Rigidbody2D>();
            hasBall = false;
            Debug.Log("✅ Ball found by tag!");
        }
    }

    // === EXIT GAME ===
    void ExitGame()
    {
        Debug.Log("Exiting game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // === VISUAL DEBUG: Show Pitch Boundaries ===
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 bottomLeft = new Vector3(minX, minY, 0);
        Vector3 topRight = new Vector3(maxX, maxY, 0);
        Vector3 bottomRight = new Vector3(maxX, minY, 0);
        Vector3 topLeft = new Vector3(minX, maxY, 0);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}