using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Vector2 lastMovementDirection = Vector2.zero;
    public void setLastMovement(Vector2 movement)
    {
        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement.normalized;
        }
        else
        {
            lastMovementDirection = Vector2.zero;  // Go back to player if they stop moving
        }
    }
}
