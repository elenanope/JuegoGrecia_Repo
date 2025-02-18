using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class AppleManager : MonoBehaviour
{

    [Header("Opción de Pick Ups")]
    [SerializeField] bool appleCount;
    [SerializeField] bool dracmaCount;
    [SerializeField] bool calizCount;


    [SerializeField] bool ultimateDialogue;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject textPanel;
    [SerializeField] int grabbedPickUps;
    [SerializeField] int goalPickUps;
    [SerializeField] GameObject wall;
    [SerializeField] float speed;
    [SerializeField] Transform[] basketPoints;
    [SerializeField] Transform[] wallPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(appleCount)
            {
                if (GameManager.Instance.apple != 0)
                {
                    grabbedPickUps += GameManager.Instance.apple;
                    //ponerRuidos
                    GameManager.Instance.apple = 0;
                    Debug.Log("Se han registrado manzanas");
                }
                else Debug.Log("No tienes nuevas manzanas");//luego poner aqui sonido de wrong
            }
            
            if(dracmaCount)
            {
                grabbedPickUps += GameManager.Instance.dracma;
                GameManager.Instance.dracma = 0;
                Debug.Log("Se han registrado dracmas");
                //sonidoMoneda
                if (grabbedPickUps < goalPickUps)
                {
                    textPanel.SetActive(true); //Gracias por x dracmas
                    text.text = grabbedPickUps.ToString();
                }
                else ultimateDialogue = true;
                    
            }
            if(calizCount)
            {
                grabbedPickUps += GameManager.Instance.caliz;
                GameManager.Instance.caliz = 0;
                Debug.Log("Se han registrado calices");
                //sonidoMoneda
                if(grabbedPickUps < goalPickUps)
                {
                    textPanel.SetActive(true); //this isn't enough! pero si te faltan, poner en script dialogo  ohh, empieza dialogo, 
                    //seguramente cambie todo esto a que lo haga el dialogo script
                }
                else ultimateDialogue = true; //referenciar en dialogo manager

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dracmaCount)
        {
            textPanel.SetActive(false); //Gracias por x dracmas
        }
        if (calizCount) textPanel.SetActive(false);
    }
    void Start()
    {

        if (appleCount)
        {
            transform.position = basketPoints[0].position;
            wall.transform.position = wallPoints[0].position;
        }
       
    }

    void Update()
    {
        if(grabbedPickUps!=0&&appleCount)
        {
            transform.position = Vector2.MoveTowards(transform.position, basketPoints[grabbedPickUps - 1].position, speed * Time.deltaTime);
            wall.transform.position = Vector2.MoveTowards(wall.transform.position, wallPoints[grabbedPickUps - 1].position, speed * Time.deltaTime);
        }
        
    }
    void DisableObject()
    {
        textPanel.SetActive(false);
    }

    
}


