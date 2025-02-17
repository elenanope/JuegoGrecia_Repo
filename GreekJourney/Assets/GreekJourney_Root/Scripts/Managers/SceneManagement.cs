using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Librería que permite la carga/descarga de escenas

public class SceneManagement : MonoBehaviour
{
    public int specificSceneToLoad;int sceneToLoad;
    public bool hadesRedemption;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) SceneManager.LoadScene(specificSceneToLoad);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&hadesRedemption) SceneManager.LoadScene(specificSceneToLoad);
    }
    public void SceneLoader(int sceneToLoad)
    {
        //Esta acción carga la escena del espacio igual al entero que indiquemos
        SceneManager.LoadScene(sceneToLoad);
    }

    public void SceneLoaderOnPlay()
    {
        SceneManager.LoadScene(specificSceneToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
