using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Move Parameters")]
    [SerializeField] float normalSpeed;
    [SerializeField] float followSpeed;
    [SerializeField] bool facingRight;
    [SerializeField] bool canDie;
    [SerializeField] bool canAttack;
    [SerializeField] Rigidbody2D enemyrb;
    [SerializeField] float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    // hará falta esto? [SerializeField] float limitX

    [Header("Enemy Game System Configuration")]
    [SerializeField] float damageToPlayer = 0.1f;
    [SerializeField] float damageTaken = 0.2f;
    [SerializeField] float enemyLife = 0.6f;

    [SerializeField] GameObject player;

    [Header("Editor References")]
    [SerializeField] Animator enemyAnim;
    [SerializeField] Animator playerAnim;

    public bool canFollow;

    [Header("PickUp References")]
    [SerializeField] GameObject pickUp;
    [SerializeField] bool lastScene;

    //Autoreferences
    //[SerializeField] GameObject enemyBody; //Cuerpo del enemigo


    // Start is called before the first frame update
    void Start()
    {
        enemyrb = GetComponent<Rigidbody2D>();
        canFollow = false;
        lastScene = false;
        canDie = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyLife <= 0) enemyLife = 0;

        if (canDie)
        {
            FlipTowardsTarget();
            if (canFollow) FollowingPlayer();
            else enemyAnim.SetBool("Walk", false);
            //antes: if (canFollow && (distanceToPlayer <= startFollowing)) FollowingPlayer();
            if (enemyLife == 0)
            {
                canDie = false;
                Dying();
            }
            if ((facingRight && player.transform.position.x - transform.position.x <= 1.5) ||
        (!facingRight && transform.position.x - player.transform.position.x <= 1.5))
            {
                // Check if enough time has passed for the next attack
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    Invoke("Attacking", 0f);  // Immediate attack call
                    lastAttackTime = Time.time;  // Update the last attack time
                }
            }
        }


        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //si despues de haber collisionado, la distancia con el colisionado es mayor a x,
        //que vuelva a su sitio y al idle, y que si entra en el true collider que ataque
        if (collision.gameObject.CompareTag("Player")&&canDie)
        {
            Attacking();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            //quitar vida?
            enemyLife -= damageTaken;
            enemyAnim.SetTrigger("Hurt");
            if(facingRight) enemyrb.AddForce(Vector3.left * 15, ForceMode2D.Impulse);
            else enemyrb.AddForce(Vector3.right * 15, ForceMode2D.Impulse);
            Debug.Log("You hurt an enemy!");
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
    void Attacking()
    {
        enemyAnim.SetTrigger("Attack");
        GameManager.Instance.totalLife -= damageToPlayer;
        playerAnim.SetTrigger("Hurt");
        Debug.Log("Jabali atacando!");
    }
    
    void Dying()
    {
        enemyAnim.SetTrigger("Death");
        //enemyCol.enabled = false;
        Debug.Log("An enemy died!");
        if (!lastScene) pickUp.SetActive(true);
        //if(!lastScene) Invoke("SpawnObject", 1);
    }
    void SpawnObject()
    {
        
        //Instantiate(pickUp, gameObject.transform.position, Quaternion.identity);
        //pickUpRb.AddForce(Vector3.up * upForce, ForceMode2D.Impulse);
        //pickUpRb.AddForce(Vector3.right * lateralForce, ForceMode2D.Impulse);
    }
    void LiberarPickUp()
    {
        Instantiate(pickUp);//ahora hay que referenciar este, no otro
        //pickUpRb.AddForce(Vector3.up * upForce, ForceMode2D.Impulse);
        //pickUpRb.AddForce(Vector3.right * lateralForce, ForceMode2D.Impulse);
    }
}
