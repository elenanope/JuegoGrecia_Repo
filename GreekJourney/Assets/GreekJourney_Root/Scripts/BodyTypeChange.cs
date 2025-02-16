using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTypeChange : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField] bool changeType;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(changeType && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
