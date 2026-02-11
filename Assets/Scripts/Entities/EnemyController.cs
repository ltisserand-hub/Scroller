using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private bool isActive = true;
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime;
    [Header("Random Settings")]
    [SerializeField] private bool _isWaitRandom;
    [SerializeField] private bool _isTargetRandom;
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private string _seed;
    private System.Random _random;
    void Start()
    {
        StartCoroutine("Movement");
        _random = new System.Random(_seed.GetHashCode());
    }

    private IEnumerator Movement()
    {
        while (isActive)
        {
            if (_isTargetRandom)
            {
                int index = _random.Next(0, _waypoints.Count);
                yield return new WaitForSeconds(_waitTime);
                Vector3 Direction = _waypoints[index].position - transform.position;
                Direction.Normalize();
                if (_isWaitRandom)
                {
                    _waitTime = _random.Next((int)_speedRange.x, (int)_speedRange.y);
                }
                while (Vector3.Distance(transform.position,_waypoints[index].position) > _speed/20f)
                {
                    transform.position += _speed * Time.deltaTime * Direction;
                    yield return new WaitForNextFrameUnit();
                }
            }
            else
            {
                foreach(Transform waypoint in _waypoints)
                {
                    yield return new WaitForSeconds(_waitTime);
                    Vector3 Direction = waypoint.position - transform.position;
                    Direction.Normalize();
                    if (_isWaitRandom)
                    {
                        _waitTime = _random.Next((int)_speedRange.x, (int)_speedRange.y);
                    }
                    while (Vector3.Distance(transform.position,waypoint.position) > _speed/20f)
                    {
                        transform.position += _speed * Time.deltaTime * Direction;
                        yield return new WaitForNextFrameUnit();
                    }
                }
            }
        }
    }

    private Vector3 ClampPosition(Vector3 position,Vector3 max,Vector3 direction)
    {
        if (position.x * Mathf.Sign(max.x) >= max.x * Mathf.Sign(max.x))
        {
            position.x = max.x;
        }

        if (position.y * Mathf.Sign(max.y) >= max.y * Mathf.Sign(max.y))
        {
            position.y = max.y;
        }
        return position;
    }
}
