using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppleManager : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject textPanel;
    [SerializeField] int grabbedApples;
    [SerializeField] GameObject wall;
    [SerializeField] float speed;
    [SerializeField] Transform[] basketPoints;
    [SerializeField] Transform[] wallPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
            if (GameManager.Instance.apple != 0)
            {
                grabbedApples += GameManager.Instance.apple;
                //ponerRuidos
                GameManager.Instance.apple = 0;
                Debug.Log("Se han registrado manzanas");
                //textPanel.SetActive(true);
                //text.text = grabbedApples.ToString();

            }
            else Debug.Log("No tienes nuevas manzanas");//luego poner aqui sonido de wrong


            //transform.position = new Vector2(grabbedApples * -transform.position.x, transform.position.y);
        }
    }

    void Start()
    {
        transform.position = basketPoints[0].position;
        wall.transform.position = wallPoints[0].position;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, basketPoints[grabbedApples].position, speed * Time.deltaTime);
        wall.transform.position = Vector2.MoveTowards(wall.transform.position, wallPoints[grabbedApples].position, speed * Time.deltaTime);
    }
    void DisableObject()
    {
        textPanel.SetActive(false);
    }

    
}


