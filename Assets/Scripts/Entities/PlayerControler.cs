using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("Speed Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxSpeed = 5f;
    [Header("Friction Settings")]
    [SerializeField] [Range(0.0f, 1.0f)] private float airFriction = 1f;
    [SerializeField] [Range(0.0f, 1.0f)] private float groundFriction = 1f;
    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] [Range(0,5)] private int maxDoubleJumps;
    [SerializeField] private float jumpBuffer;
    [Header("Active")]
    [SerializeField] public bool isActive = true;
    
    private bool isGrounded;
    private int _currentDoubleJumps;
    private float horizontal;
    private float _lastJumpBuffer;
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
                    rb.AddForceY(jumpHeight / 10000, ForceMode2D.Impulse);
                    _groundedCheck.currentTime = _groundedCheck.coyoteTime;
                }
                else if (_currentDoubleJumps >= 1) //Use double Jump
                {
                    _currentDoubleJumps = _currentDoubleJumps - 1;
                    rb.linearVelocityY = 0;
                    rb.AddForceY(jumpHeight / 10000, ForceMode2D.Impulse);
                }
                else
                {
                    _lastJumpBuffer = Time.time;
                }
            }
        }
    }

    void Update()
    {
        if (isGrounded != _groundedCheck.IsGrounded() && Time.time  - _lastJumpBuffer <= jumpBuffer && _lastJumpBuffer > 0f )
        {
            rb.linearVelocityY = 0;
            rb.AddForceY(jumpHeight / 10000, ForceMode2D.Impulse);
            _lastJumpBuffer = 0f;
        }
        
        isGrounded = _groundedCheck.IsGrounded();
        //Process Movement

        Mouvement();

        //Double Jump Reset

        if (_currentDoubleJumps != maxDoubleJumps)
        {
            if (isGrounded)
            {
                _currentDoubleJumps = maxDoubleJumps;
            }
        }

        // Animator

        int aHorizontal = (int)rb.linearVelocity.x;
        _animator.SetInteger("Horizontal", aHorizontal);
        _animator.SetBool("Grounded", _groundedCheck.IsGrounded());
        _animator.SetFloat("Vertical", rb.linearVelocity.y);

        //Freeze is not active

        if (isActive)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

    void Mouvement()
    {
        if (isActive)
        {
            if (isGrounded)
            {
                if (horizontal != 0)
                {
                    rb.AddForceX(
                        Mathf.Abs(rb.linearVelocityX * horizontal - maxSpeed) * speed * horizontal / 10000 * Time.deltaTime * groundFriction , ForceMode2D.Impulse); 
                }

                rb.linearVelocityX *= 1 -(groundFriction * Time.deltaTime * maxSpeed);
            }
            else
            {
                if (horizontal != 0)
                {
                        rb.AddForceX(
                            Mathf.Abs(rb.linearVelocityX * horizontal - maxSpeed) * speed * horizontal / 10000 * Time.deltaTime * airFriction , ForceMode2D.Impulse);
                }
                rb.linearVelocityX *= 1 -(airFriction * Time.deltaTime * maxSpeed);
            }
        }
    }
}
