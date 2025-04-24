using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float HP { get; set; } = 100.0f;

    private Rigidbody2D rb;
    private PlayerControlInput inputActions;
    private Animator animator;

    private Vector2 inputVector;

    private float moveSpeed = 5.0f;
    private float jumpPower = 150.0f;

    private bool isGrounded = false;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;

    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputActions = new PlayerControlInput();


        inputActions.Player.Move.performed += ctx =>
        {
            inputVector = ctx.ReadValue<Vector2>();

            if (inputVector.x > 0)
            {
                // transform.localScale.x = Mathf.Abs(transform.localScale.x); ÀÌ°Ç ¾ÈµÊ
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1.0f, transform.localScale.y, transform.localScale.z);
            }
        };
        inputActions.Player.Move.canceled += ctx => inputVector = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => 
        {
            if (!isGrounded) return;

            animator.SetTrigger("Jump");
            rb.AddForceY(jumpPower);
        };

    }
    private void Update()
    {
        // sqrMagnitudeÀº º¤ÅÍÀÇ ±æÀÌ Á¦°ö°ª. ¿¬»êÀÌ magnitudeº¸´Ù °¡º­¿ò
        animator.SetBool("IsRun", inputVector.sqrMagnitude > 0.01f);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = inputVector.x * moveSpeed;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
