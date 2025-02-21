using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement; //Librería que permite la carga/descarga de escenas

public class SceneManagement : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public int specificSceneToLoad;int sceneToLoad;
    public bool hadesRedemption;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&!hadesRedemption)
        {
            audioSource.Stop(); 
            SceneManager.LoadScene(specificSceneToLoad);
        }
        if (collision.gameObject.CompareTag("Weapon"))
        {
            audioSource.Stop();
            SceneManager.LoadScene(specificSceneToLoad);
        }

    }

    public void SceneLoader(int sceneToLoad)
    {
        //audioSource.Stop();
        //Esta acción carga la escena del espacio igual al entero que indiquemos
        SceneManager.LoadScene(sceneToLoad);
    }

    public void SceneLoaderOnPlay()
    {
        audioSource.Stop();
        SceneManager.LoadScene(specificSceneToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
