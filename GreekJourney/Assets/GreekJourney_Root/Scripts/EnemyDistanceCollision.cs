using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceCollision : MonoBehaviour
{
    [SerializeField] EnemyAI scriptEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //scriptEnemy.canFollow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //scriptEnemy.canFollow = false;
        }
    }
}