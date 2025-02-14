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

    GameObject player;

    [Header("Editor References")]
    [SerializeField] Animator enemyAnim;

    //Autoreferences
    BoxCollider2D enemyCol; //Collider más pequeño del enemigo
    [SerializeField] GameObject enemyBody; //Cuerpo del enemigo
    BoxCollider2D distanceCol; //Collider de distancia 


    // Start is called before the first frame update
    void Start()
    {
        enemyCol = GetComponent<BoxCollider2D>();
        distanceCol = enemyBody.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //si despues de haber collisionado, la distancia con el colisionado es mayor a x,
        //que vuelva a su sitio y al idle, y que si entra en el true collider que ataque
        if (collision.gameObject.CompareTag("Player"))
        {
            FollowingPlayer();
            //if(esta x cerca del player

        }
    }



    void FollowingPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= startFollowing)
        {
            // que siga al player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followSpeed * Time.deltaTime);
        }
    }

    void Attacking()
    {
        //hacer en otro script para un empty?
    }

}
