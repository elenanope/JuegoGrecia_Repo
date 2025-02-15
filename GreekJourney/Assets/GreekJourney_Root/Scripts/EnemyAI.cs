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
    [SerializeField] float startFollowing;
    // hará falta esto? [SerializeField] float limitX

    [Header("Enemy Game System Configuration")]
    [SerializeField] int pointsToGive = 0;
    [SerializeField] float lifeToTake = 0.1f;
    [SerializeField] float enemyLife = 0.6f;

    [SerializeField] GameObject player;

    [Header("Editor References")]
    [SerializeField] Animator enemyAnim;

    public bool canFollow;
    float distanceToPlayer;

    //Autoreferences
    //[SerializeField] GameObject enemyBody; //Cuerpo del enemigo
    [SerializeField] BoxCollider2D enemyCol;


    // Start is called before the first frame update
    void Start()
    {
        enemyCol = GetComponent<BoxCollider2D>();
        canFollow = false;
        canDie = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFollow) FollowingPlayer();
        else enemyAnim.SetBool("Walk", false);
        //antes: if (canFollow && (distanceToPlayer <= startFollowing)) FollowingPlayer();
        

        if(enemyLife<=0) enemyLife = 0;
        if(enemyLife == 0 && canDie)
        {
            enemyAnim.SetTrigger("Death");
            //enemyCol.enabled = false;
            Debug.Log("An enemy died!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //si despues de haber collisionado, la distancia con el colisionado es mayor a x,
        //que vuelva a su sitio y al idle, y que si entra en el true collider que ataque
        if (collision.gameObject.CompareTag("Player"))
        {
            Attacking();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon")) //Aun falta crear espada con collider y eso
        {
            //quitar vida?
            enemyLife -= 0.2f;
            enemyAnim.SetTrigger("Hurt");
            Debug.Log("You hurt an enemy!");
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
        GameManager.Instance.totalLife -= lifeToTake;
        Debug.Log("Jabali atacando!");
    }

}
