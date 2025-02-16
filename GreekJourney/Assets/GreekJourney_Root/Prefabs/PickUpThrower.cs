using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpThrower : MonoBehaviour
{
    public Rigidbody2D pickUpRb;
    public GameObject pickUp;
    public float upForce; public float lateralForce;
    
    void LiberarPickUp()
    {
        Instantiate(pickUp);
        pickUpRb.AddForce(Vector3.up * upForce, ForceMode2D.Impulse);
        pickUpRb.AddForce(Vector3.right * lateralForce, ForceMode2D.Impulse);
    }
}
