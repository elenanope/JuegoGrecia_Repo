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
    [SerializeField] float startFollowing;
    // hará falta esto? [SerializeField] float limitX

    [Header("Enemy Game System Configuration")]
    [SerializeField] int pointsToGive = 0;
    [SerializeField] float lifeToTake = 0.1f;
    [SerializeField] float enemyLife = 1f;

    GameObject player;

    [Header("Editor References")]
    [SerializeField] Animator enemyAnim;

    public bool canFollow;
    float distanceToPlayer;

    //Autoreferences
    [SerializeField] GameObject enemyBody; //Cuerpo del enemigo


    // Start is called before the first frame update
    void Start()
    {
        canFollow = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (canFollow && (distanceToPlayer <= startFollowing)) FollowingPlayer();
        //else???

        //if (enemyLife cambia) enemyAnim.SetTrigger("Hurt");;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //si despues de haber collisionado, la distancia con el colisionado es mayor a x,
        //que vuelva a su sitio y al idle, y que si entra en el true collider que ataque
        if (collision.gameObject.CompareTag("Player"))
        {
            Attacking();
            //if(esta x cerca del player

        }
    }



    void FollowingPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followSpeed * Time.deltaTime);
    }

    void Attacking()
    {
        enemyAnim.SetTrigger("Attack");
        //gameManager.Instance.totalLife - lifeToTake;
    }

}
