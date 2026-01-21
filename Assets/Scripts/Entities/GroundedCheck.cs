using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    public float coyoteTime;
    public float currentTime;

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            currentTime = 0f;
            return true;
        }
        else if (currentTime < coyoteTime)
        {
            currentTime += Time.deltaTime;
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}