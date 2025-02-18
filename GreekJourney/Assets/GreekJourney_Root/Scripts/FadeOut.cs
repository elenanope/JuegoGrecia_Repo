using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField]AppleManager appleManager;
    public float fadeDuration = 2f; // Time for fading out
    private Image image; // Image component to manipulate alpha
    //private Color originalColor;
    private float elapsedTime = 0f;
    bool dontRepeat;

    public float fadeOutDuration = 3f;
    public float fadeInDuration = 3f;

    private void Start()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        if(appleManager.movingSth&&!dontRepeat) dontRepeat = true;StartCoroutine(nameof(StartGameFadeOut));
    }
    


    IEnumerator FadingOut()
    {
        Color originalColor = image.color; // Store the original color

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime; // Increase the elapsed time
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Calculate the alpha value
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // Update the color with new alpha
            yield return null;  // Wait for the next frame
        }
    }
    
    IEnumerator FadingIn()
    {
        Color originalColor = image.color; // Store the original color

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime; // Increase the elapsed time
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // Calculate the alpha value
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // Update the color with new alpha
            yield return null;  // Wait for the next frame
        }
    }
    

    private IEnumerator StartGameFadeOut()
    {
        yield return new WaitForSeconds(1);
        FadingOut();
        yield return new WaitForSeconds(3);
        FadingIn();
        yield return null;
    }
}
