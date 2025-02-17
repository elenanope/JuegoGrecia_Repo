using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float fadeDuration = 2f; // Time for fading out
    private Image sr; // Image component to manipulate alpha
    //private Color originalColor;
    private float elapsedTime = 0f;
    bool alarma;

    // Called once when the script is first initialized
    void Start()
    {
        StopCoroutine(FadingOut());
        elapsedTime = 0;
        alarma = false;
        StartCoroutine(FadingOut());  // Start the fade-out coroutine
    }

    private void Update()
    {
        if(alarma)gameObject.SetActive(false);  // Deactivate the object after the fade is complete
    }

    // Coroutine to handle fading out
    IEnumerator FadingOut()
    {
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }
        alarma = true;
    }
}
