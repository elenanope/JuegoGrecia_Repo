using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PickUpPoints : MonoBehaviour
{
    [SerializeField]GameplayUIHandler handlerUI;
    public int pointSum;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PointsUp(pointSum);
            handlerUI.pickUps += 1; 
            gameObject.SetActive(false);
        }
    }
    
}
