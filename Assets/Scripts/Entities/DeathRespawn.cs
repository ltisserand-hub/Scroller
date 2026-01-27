using System;
using UnityEngine;

public class DeathRespawn : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] public Vector2 respawnPosition;
    [SerializeField] int respawnTime = 100;
    [Header("HitBox Settings")]
    [SerializeField] public Vector2 boxSize;
    [SerializeField] public float castDistance;
    [SerializeField] public LayerMask layer;
    [Header("Visual Settings")]
    [SerializeField] private Color Tint;
    private int currentTime = 0;
    private Transform trans;

    public void Respawn()
    {
        //play death animation
        currentTime = 1;
        GetComponent<SpriteRenderer>().color = Tint;
        GetComponent<PlayerControler>().isActive = false;
    }

    public void FixedUpdate()
    {
        if (currentTime >= 1)
        {
            if (currentTime == respawnTime)
            {
                currentTime = 0;
                GetComponent<SpriteRenderer>().color = Color.white;
                GetComponent<Transform>().position = respawnPosition;
                GetComponent<PlayerControler>().isActive = true;
            }
            else
            {
                currentTime = currentTime + 1;
            }
        }

        if (GetComponent<PlayerControler>().isActive)
        {
            if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, layer))
            {
                Respawn();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }
}
