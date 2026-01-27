using System;
using UnityEngine;
public class SmoothFollow : MonoBehaviour
{ 
    [Header("Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0f,0f,-10f);
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private Transform target;
    [Header("Velocity based multipliers")]
    [SerializeField] private Rigidbody2D velocityOffset;
    [SerializeField][InspectorName("Target")] private Vector2 velocityOffsetMultiplier;

    private Vector3 targetPos;

    private void Start()
    {
        if (target != null)
        {
            if (velocityOffset != null)
            {
                transform.position = target.position + offset + new Vector3(velocityOffset.linearVelocityX*velocityOffsetMultiplier.x, velocityOffset.linearVelocityY*velocityOffsetMultiplier.y);
            }
            else
            {
                transform.position = target.position + offset;
            }
        }
        else
        {
            Debug.LogError("Target not found in \""+ this.gameObject.name +"\" for SmoothFollow.cs : Please kindly add a target for the object to follow");
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (velocityOffset != null)
            {
                targetPos = target.position + offset + new Vector3(velocityOffset.linearVelocityX*velocityOffsetMultiplier.x, velocityOffset.linearVelocityY*velocityOffsetMultiplier.y);
            }
            else
            {
                targetPos = target.position + offset;
            }
        }
        else
        {
            Debug.LogError("Target not found in \""+ this.gameObject.name +"\" for SmoothFollow.cs : Please kindly add a target for the object to follow");
            targetPos = Vector3.zero;
        }
        transform.position = Vector3.SmoothDamp(transform.position , targetPos, ref _velocity, smoothTime);
        
    }
}
