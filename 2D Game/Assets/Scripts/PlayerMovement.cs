using System;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CharacterMovement
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private Transform graphics;
    [SerializeField] private Transform helpers;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Animator animator;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        InputManager.JumpAction += OnJump;
    }

    private void OnDestroy()
    {
        InputManager.JumpAction -= OnJump;
    }

    private void FixedUpdate()
    {
        if (IsFrozen)
        {
            Vector2 velocity = rigidbody.velocity;
            velocity.x = 0f;
            rigidbody.velocity = velocity;
            return;
        }
        
        Vector2 direction = new Vector2(InputManager.HorizontalAxis, 0f);

        if (!IsGrounded())
        {
            direction *= 0.5f;
        }
        Move(direction);
    }

    private void Update()
    {
        if (IsGrounded())
        {
            animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.x));
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
        
        if (Mathf.Abs(rigidbody.velocity.x) < 0.01f)
        {
            return;
        }
        
        float xScale = rigidbody.velocity.x > 0 ? 1f : -1f;
        if (xScale < 0f && graphics.localScale.x < 0f)
        {
            return;
        }
        
        if (xScale > 0 && graphics.localScale.x > 0f)
        {
            return;
        }

        graphics.localScale = new Vector3(xScale, 1f, 1f);

        float xAngle = rigidbody.velocity.x > 0f ? 0f : 180f;
        helpers.localEulerAngles = new Vector3(0f, xAngle, 0f);
    }


    private bool IsGrounded()
    {
        Vector2 point = transform.position;
        point.y -= 0.1f;//чтобы избежать рейкаста в самого себя
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.down, 0.2f);
        return hit.collider != null;
    }

    public override void Move(Vector2 direction)
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.x = direction.x * maxSpeed;
        rigidbody.velocity = velocity;
    }
    public override void Stop(float timer)
    {
        
    }


    public override void Jump(float force)
    {
        rigidbody.AddForce(new Vector2(0f, force), ForceMode2D.Impulse);
    }

    private void OnJump(float inputForce)
    {
        if (IsGrounded())
        {
            animator.SetTrigger("Jump");
            Jump(inputForce * jumpForce);
        }
    }
}