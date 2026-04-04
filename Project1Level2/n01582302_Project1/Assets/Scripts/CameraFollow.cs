using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraSpeed = 8.0f;
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);

    // Added for look ahead
    Vector3 currentLookAhead;
    BasicMovement move;
    public float lookAheadDistance = 3.0f;
    public float lookAheadSpeed = 6.0f;

    private void Awake()
    {
        // Grabbing the components from the taregt
        move = target.GetComponent<BasicMovement>();
    }



    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 targetLookAhead = new Vector3(move.lastMovementDirection.x,// x 
                                                0, // y
                                                    0) * lookAheadDistance;

        currentLookAhead = Vector3.Lerp(currentLookAhead, // Start 
                                                targetLookAhead, // End
                                                lookAheadSpeed * Time.fixedDeltaTime); // How far

        // Since its a platformer, ignore y changes, only horizontal movement
        Vector3 targetPosition = new Vector3(target.position.x, 0, -10); 

        Vector3 desiredPosition = targetPosition + currentLookAhead;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, // Start 
                                                desiredPosition, // End
                                                cameraSpeed * Time.fixedDeltaTime); // How far

        transform.position = smoothedPosition;
    }
}
