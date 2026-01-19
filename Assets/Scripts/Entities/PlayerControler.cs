using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D rb;    
    [SerializeField] private float speed = 5;
    [SerializeField][Range(0.0f, 1.0f)] private float airFriction = 1f;
    [SerializeField][Range(0.0f, 1.0f)] private float groundFriction = 1f;
    
    
    
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private int maxDoubleJumps;
    [SerializeField] public bool isActive =  true;
    
    
    
    private bool isGrounded;
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
        if (isActive) //disable input if frozen
        {
            horizontal = context.ReadValue<Vector2>().x; //Fetch horizontal value from input
        }
        else
        {
            horizontal = 0; //freeze movement
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isActive) //Don't jump if frozen
            {
                if (isGrounded) //Normal Jump
                {
                    rb.AddForceX(jumpHeight/10000, ForceMode2D.Impulse);
                }
                else if(_currentDoubleJumps>=1) //Use double Jump
                {
                    _currentDoubleJumps = _currentDoubleJumps - 1;
                    rb.linearVelocityY = 0;
                    rb.AddForceX(jumpHeight/10000, ForceMode2D.Impulse);
                }
            }
        }
    }
    
    void Update()
    {
        isGrounded = _groundedCheck.IsGrounded();
        
        //Process Movement

        if (isActive)
        {
            if (isGrounded)
            {
                if (horizontal != 0)
                {
                    rb.AddForceX(horizontal * (speed/10000) * Time.deltaTime, ForceMode2D.Impulse);
                }
                else if(rb.linearVelocityX> 0)
                {
                    rb.AddForceX((rb.linearVelocityX/(rb.linearVelocityX *rb.linearVelocityX)) * (speed/10000) * Time.deltaTime, ForceMode2D.Impulse);
                }
            }
        }
        
        
        
        // rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocityY);
        
        //Double Jump Reset
        
        if (_currentDoubleJumps != maxDoubleJumps)
        {
            if (isGrounded)
            {
                _currentDoubleJumps = maxDoubleJumps;
            }
        }
        print(rb.linearVelocity);
        
        // Animator
        
        int aHorizontal = (int)rb.linearVelocity.x;
        _animator.SetInteger("Horizontal", aHorizontal);
        _animator.SetBool("Grounded", _groundedCheck.IsGrounded());
        _animator.SetFloat("Vertical", rb.linearVelocity.y);
        
        //Freeze is not active
        
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
