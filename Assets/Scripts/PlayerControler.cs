using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D rb;    
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private int maxDoubleJumps;
    [SerializeField] public bool isActive =  true;
    private int _currentDoubleJumps;
    
    private float horizontal;

    private GroundedCheck _groundedCheck;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _groundedCheck = GetComponent<GroundedCheck>();
        
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        if (isActive)
        {
            horizontal = context.ReadValue<Vector2>().x;
        }
        else
        {
            horizontal = 0;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isActive)
            {
                if (_groundedCheck.IsGrounded())
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpHeight);
                }
                else if(_currentDoubleJumps>=1)
                {
                    _currentDoubleJumps = _currentDoubleJumps - 1;
                    rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpHeight);
                }
            }
        }
    }
    
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocityY);
        
        if (_currentDoubleJumps != maxDoubleJumps)
        {
            if (_groundedCheck.IsGrounded())
            {
                _currentDoubleJumps = maxDoubleJumps;
            }
        }
        
        int aHorizontal = (int)rb.linearVelocity.x;
        _animator.SetInteger("Horizontal", aHorizontal);
        _animator.SetBool("Grounded", _groundedCheck.IsGrounded());
        _animator.SetFloat("Vertical", rb.linearVelocity.y);
        if (isActive)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation ;
        }
        else
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll ;
            rb.linearVelocity = new Vector2(0, 0);
        }
    }
}
