using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Referecias privadas generales
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] PlayerInput playerInput;
    Vector2 moveInput;
    public bool isAttacking;


    [Header("Run Parameters")]
    public float speed;
    [SerializeField] bool isFacingRight;

    [Header("Jump Parameters")]
    public float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;

    private Animator playerAnim;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>();
        isFacingRight = true;
    }

    void Update()
    {
        GroundCheck();
        if (moveInput.x > 0 && !isFacingRight) Flip();
        if (moveInput.x < 0 && isFacingRight) Flip();
    }

    private void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        playerRB.velocity = new Vector3(moveInput.x * speed, playerRB.velocity.y, 0);
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    void GroundCheck()
    {
        //isGrounded es verdadero cuando el circulo detector toque la layer ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }

    #region Input Methods
    //Métodos que permiten leer el input del New Input System
    //Crearemos un método por cada acción

    public void Run(InputAction.CallbackContext context)
    {
        //Las acciones de tpo VALUE deben almacenarse = ReadValue
        moveInput = context.ReadValue<Vector2>();
        if (context.started)
        {
            playerAnim.SetBool("Run", true);
        }
        if (context.canceled)
        {
            playerAnim.SetBool("Run", false);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isGrounded)
            {   
                playerAnim.SetBool("Jump", true);
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        if (context.canceled)
        {
            playerAnim.SetBool("Jump", false);
        }

    }
    public void Attack1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isAttacking = true;
            playerAnim.SetTrigger("Attack1");
        }


    }
    public void Attack2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isAttacking = true;
            playerAnim.SetTrigger("Attack2");
        }
    }
    #endregion
}
