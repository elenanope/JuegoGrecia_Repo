using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [Header("Enemy Type")]
    [SerializeField] bool wildPig;
    [SerializeField] bool crow;
    [SerializeField] bool spartan;
    [SerializeField] bool demon;

    [Header("Enemy Move Parameters")]
    [SerializeField] float normalSpeed;
    [SerializeField] float followSpeed;
    [SerializeField] bool facingRight;
    [SerializeField] bool canDie;
    
    [SerializeField] Rigidbody2D enemyrb;
    // hará falta esto? [SerializeField] float limitX

    [Header("Enemy Game System Configuration")]
    [SerializeField] float damageToPlayer = 0.1f;
    [SerializeField] float damageTaken = 0.2f;
    [SerializeField] float enemyLife = 0.6f;

    [SerializeField] float attackDistance = 1f;
    [SerializeField] float attackCooldown = 0f;
    public bool attacking;

    [SerializeField] GameObject player;

    [Header("Editor References")]
    [SerializeField] Animator enemyAnim;

    public bool canFollow;

    [Header("PickUp References")]
    [SerializeField] GameObject pickUp;
    [SerializeField] bool soltarPickUp;

    void Start()
    {
        enemyrb = GetComponent<Rigidbody2D>();
        canFollow = false;
        canDie = true;
    }
    void Update()
    {
        if (enemyLife <= 0) enemyLife = 0;

        if (canDie)
        {
            FlipTowardsTarget();
            if (Vector2.Distance(transform.position, player.transform.position) < 3f && Vector2.Distance(transform.position, player.transform.position) > attackDistance) FollowingPlayer();
            else if (Vector2.Distance(transform.position, player.transform.position) < attackDistance && attackCooldown == 0) StartCoroutine(Attacking());
            else enemyAnim.SetBool("Walk", false);
            if (enemyLife == 0)
            {
                canDie = false;
                Dying();
                if (soltarPickUp) pickUp.SetActive(true);
            }
        }  
        if(attackCooldown > 0) attackCooldown -= Time.deltaTime;
        else attackCooldown = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon")&&canDie)
        {
            enemyLife -= damageTaken;
            enemyAnim.SetTrigger("Hurt");
            if(facingRight) enemyrb.AddForce(Vector3.left * 15, ForceMode2D.Impulse);
            else enemyrb.AddForce(Vector3.right * 15, ForceMode2D.Impulse);
            Debug.Log("You hurt an enemy!");
            if (wildPig) AudioManager.Instance.PlaySFX(12);
            if (crow) AudioManager.Instance.PlaySFX(11);
            if (demon) AudioManager.Instance.PlaySFX(14);
            if (spartan) AudioManager.Instance.PlaySFX(13);
        }
    }
    void FlipTowardsTarget()
    {
        Vector3 scale = transform.localScale;

        if ((player.transform.position.x > transform.position.x) != facingRight)
        {
            facingRight = !facingRight;
            scale.x *= -1; // Invierte la escala en X
            transform.localScale = scale;
        }
    }
    void FollowingPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followSpeed * Time.deltaTime);
        enemyAnim.SetBool("Walk",true );
        Debug.Log("Siguiendo al player");
    }
    IEnumerator Attacking()
    {
        attackCooldown = 1;
        enemyAnim.ResetTrigger("Attack");
        attacking = true;
        enemyAnim.SetTrigger("Attack");
        player.GetComponent<PlayerController>().TakeDamage(damageToPlayer);
        Debug.Log(name + " atacando!");
        attacking = false;
        yield break;
    }
    
    void Dying() //comprobar correcto funcionamiento
    {
        enemyAnim.SetTrigger("Death");
        Debug.Log("An enemy died!");
    }
}
