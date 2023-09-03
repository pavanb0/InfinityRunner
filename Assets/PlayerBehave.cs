using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform[] groundPlatforms; // An array of your three ground platforms
    public float moveSpeed = 5.0f; // Adjust this to control the player's movement speed
    public float jumpForce = 5.0f; // Adjust this to control the jump force

    private int currentPlatformIndex = 1; // Start on the middle ground platform
    private Rigidbody rb;
    private bool isJumping = false;
    private Vector2 touchStartPos;
    private float swipeThreshold = 50.0f;

    private void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Handle touch input for swiping
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Record the starting position of the swipe
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    // Calculate the swipe distance
                    float swipeDistance = touch.position.x - touchStartPos.x;

                    // Determine the swipe direction (left or right)
                    if (Mathf.Abs(swipeDistance) > swipeThreshold)
                    {
                        if (swipeDistance > 0 && currentPlatformIndex > 0)
                        {
                            // Swipe right, move the player and camera to the right ground platform
                            currentPlatformIndex--;
                            UpdatePosition();
                        }
                        else if (swipeDistance < 0 && currentPlatformIndex < 2)
                        {
                            // Swipe left, move the player and camera to the left ground platform
                            currentPlatformIndex++;
                            UpdatePosition();
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    // Handle tap to jump
                    if (!isJumping)
                    {
                        Jump();
                    }
                    break;
            }
        }
    }

    private void UpdatePosition()
    {
        // Move the player and camera to the selected ground platform
        Vector3 targetPosition = groundPlatforms[currentPlatformIndex].position;
        targetPosition.y = transform.position.y; // Maintain the same vertical position
        transform.position = targetPosition;
        Camera.main.transform.position = new Vector3(targetPosition.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    private void Jump()
    {
        // Apply a vertical force to make the player jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset the jumping state when the player lands
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
