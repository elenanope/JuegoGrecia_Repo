using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Referencias privadas generales
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] PlayerInput playerInput;
    Vector2 moveInput;
    public bool isAttacking;

    [Header("Run Parameters")]
    public float speed;
    [SerializeField] bool isFacingRight;
    [SerializeField] bool isAlive;

    [Header("Jump Parameters")]
    public float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;

    private Animator playerAnim;

    // Nuevas variables para materiales de física
    [SerializeField] PhysicsMaterial2D noFrictionMaterial; // Material sin fricción
    [SerializeField] PhysicsMaterial2D normalMaterial; // Material con fricción normal

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>();
        isFacingRight = true;
        isAlive = true;
    }

    void Update()
    {
        GroundCheck();
        AnimationHandler();
        if (moveInput.x > 0 && !isFacingRight) Flip();
        if (moveInput.x < 0 && isFacingRight) Flip();
        if (GameManager.Instance.totalLife == 0 && isAlive)
        {
            playerAnim.SetTrigger("Death");
            isAlive = false;
        }
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
        // isGrounded es verdadero cuando el círculo detector toca la layer ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }

    void AnimationHandler()
    {
        if (moveInput.x != 0) playerAnim.SetBool("Run", true);
        else playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Jump", !isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            GameManager.Instance.apple += 1;
            Debug.Log("pick up recogido");
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Dracma"))
        {
            GameManager.Instance.dracma += 1;
            Debug.Log("pick up recogido");
            collision.gameObject.SetActive(false);
        }
            
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) playerAnim.SetTrigger("Hurt");
    }


    #region Input Methods
    // Métodos que permiten leer el input del New Input System
    public void Run(InputAction.CallbackContext context)
    {
        // Las acciones de tipo VALUE deben almacenarse con ReadValue
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isGrounded)
            {
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    public void Attack1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;
            playerAnim.SetTrigger("Attack1");
        }
        if (context.canceled)
        {
            isAttacking = false;
        }
    }

    public void Attack2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;
            playerAnim.SetTrigger("Attack2");
        }
        if (context.canceled)
        {
            isAttacking = false;
        }
    }
    #endregion
}
