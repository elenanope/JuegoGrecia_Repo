using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public int specificSceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneLoaderOnPlay();
        }
    }


    public void SceneLoader(int sceneToLoad)
    {
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
