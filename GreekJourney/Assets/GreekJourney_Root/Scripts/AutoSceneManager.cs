using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneManager : MonoBehaviour
{
    [SerializeField] int secondsToWait;
    [SerializeField] int nextScene;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndScene());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadScene(nextScene);
    }
}