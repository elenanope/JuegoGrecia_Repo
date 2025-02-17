using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Move Parameters")]
    [SerializeField] float normalSpeed;
    [SerializeField] float followSpeed;
    [SerializeField] bool isDead;
    [SerializeField] bool facingRight;
    [SerializeField] bool canDie;
    // hará falta esto? [SerializeField] float limitX

    [Header("Enemy Game System Configuration")]
    [SerializeField] float damageToPlayer = 0.1f;
    [SerializeField] float damageTaken = 0.2f;
    [SerializeField] float enemyLife = 0.6f;

    [SerializeField] GameObject player;

    [Header("Editor References")]
    [SerializeField] Animator enemyAnim;

    public bool canFollow;

    [Header("PickUp References")]
    [SerializeField] Rigidbody2D pickUpRb;
    [SerializeField] GameObject pickUp;
    [SerializeField] float upForce=1;
    [SerializeField] float lateralForce=1;
    [SerializeField] bool lastScene;

    //Autoreferences
    //[SerializeField] GameObject enemyBody; //Cuerpo del enemigo


    // Start is called before the first frame update
    void Start()
    {
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
        if (collision.gameObject.CompareTag("Weapon")) //Aun falta crear espada con collider y eso
        {
            //quitar vida?
            enemyLife -= damageTaken;
            enemyAnim.SetTrigger("Hurt");
            Debug.Log("You hurt an enemy!");
        }
    }
    void FlipTowardsTarget()
    {
        Vector3 scale = transform.localScale;

        if(facingRight)
        {
            if (player.transform.position.x > transform.position.x)
                scale.x = Mathf.Abs(scale.x); // Mirar a la derecha
            else
                scale.x = -Mathf.Abs(scale.x); // Mirar a la izquierda

            transform.localScale = scale;
        }
        else
        {
            if (player.transform.position.x > transform.position.x)
                scale.x = -Mathf.Abs(scale.x); // Mirar a la derecha
            else
                scale.x = Mathf.Abs(scale.x); // Mirar a la izquierda

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
        Debug.Log("Jabali atacando!");
    }
    
    void Dying()
    {
        enemyAnim.SetTrigger("Death");
        //enemyCol.enabled = false;
        Debug.Log("An enemy died!");
        if (!lastScene) Instantiate(pickUp);
        //if(!lastScene) Invoke("SpawnObject", 1);
    }
    void SpawnObject()
    {
        Instantiate(pickUp, gameObject.transform.position, Quaternion.identity);
        pickUpRb.AddForce(Vector3.up * upForce, ForceMode2D.Impulse);
        //pickUpRb.AddForce(Vector3.right * lateralForce, ForceMode2D.Impulse);
    }
    void LiberarPickUp()
    {
        Instantiate(pickUp);//ahora hay que referenciar este, no otro
        pickUpRb.AddForce(Vector3.up * upForce, ForceMode2D.Impulse);
        pickUpRb.AddForce(Vector3.right * lateralForce, ForceMode2D.Impulse);
    }
}
