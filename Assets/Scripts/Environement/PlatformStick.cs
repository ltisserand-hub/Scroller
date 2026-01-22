using UnityEngine;

public class PlatformStick : MonoBehaviour
{
    [SerializeField] float castDistance;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask layer;
    
    private Vector2 lastPos;
    private Vector2 currentPos;
    private Vector2 velocity;
    void FixedUpdate()
    {
        lastPos = currentPos;
        currentPos = transform.position;
        velocity = currentPos - lastPos;
        
        if (Physics2D.BoxCast(transform.position, boxSize, 0, transform.up, castDistance, layer))
        {
            target.position +=  new Vector3(velocity.x, velocity.y);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.moccasin;
        Gizmos.DrawWireCube(transform.position+transform.up * castDistance, boxSize);
    }
}
