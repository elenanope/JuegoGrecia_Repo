using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
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

    public Transform spawnPoint;
    public float fallLimit = -10f;


    [SerializeField]GameplayUIHandler handlerUI;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>();
        handlerUI.pickUps = 0;
        isFacingRight = true;
        isAlive = true;
    }

    void Update()
    {
        GroundCheck();
        AnimationHandler();

        if (transform.position.y < fallLimit)
        {
            Respawn();
        }

        if (GameManager.Instance.totalLife == 0 && isAlive)
        {
            playerAnim.SetTrigger("Death");
            isAlive = false;
            StartCoroutine(LoadDieScene());
        }

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
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }

    void AnimationHandler()
    {
        if (moveInput.x != 0) playerAnim.SetBool("Run", true);
        else playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Jump", !isGrounded);
    }

    void Respawn()
    {
        transform.position = spawnPoint.position;
        AudioManager.Instance.PlaySFX(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.apple += 1;
            handlerUI.pickUps += 1;
            AudioManager.Instance.PlaySFX(6);
        }
        if (collision.gameObject.CompareTag("Dracma"))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.dracma += 1;
            handlerUI.pickUps += 1;
            AudioManager.Instance.PlaySFX(7);
        }
        if (collision.gameObject.CompareTag("Caliz"))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.caliz += 1;
            handlerUI.pickUps += 1;
            AudioManager.Instance.PlaySFX(8);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) playerAnim.SetTrigger("Hurt"); AudioManager.Instance.PlaySFX(3);
    }

    #region Input Methods
    public void Run(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isGrounded)
            {
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                AudioManager.Instance.PlaySFX(2);
            }
        }
    }

    public void Attack1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;
            playerAnim.SetTrigger("Attack1");
            AudioManager.Instance.PlaySFX(4);
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
            AudioManager.Instance.PlaySFX(5);
        }
        if (context.canceled)
        {
            isAttacking = false;
        }
    }
    #endregion

    private IEnumerator LoadDieScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("DieScene");
    }
}
