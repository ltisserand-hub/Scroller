using System;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{ 
    [SerializeField] private Vector3 offset = new Vector3(0f,0f,-10f);
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 velocityOffsetMultiplier;
    [SerializeField] private Rigidbody2D velocityOffset;

    private void Start()
    {
        transform.position = target.position + offset + new Vector3(velocityOffset.linearVelocityX*velocityOffsetMultiplier.x, velocityOffset.linearVelocityY*velocityOffsetMultiplier.y);
    }

    void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset + new Vector3(velocityOffset.linearVelocityX*velocityOffsetMultiplier.x, velocityOffset.linearVelocityY*velocityOffsetMultiplier.y);
        transform.position = Vector3.SmoothDamp(transform.position , targetPos, ref _velocity, smoothTime);
    }
}
