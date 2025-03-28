using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerControlInput inputActions;
    private Vector2 inputVector;
    private float moveSpeed = 5.0f;

    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        inputActions = new PlayerControlInput();

        inputActions.Player.Move.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => inputVector = Vector2.zero;
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
