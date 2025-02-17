using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float fadeDuration = 2f; // Tiempo en segundos hasta desaparecer

    void Start()
    {
        StartCoroutine(FadingOut());
    }

    IEnumerator FadingOut()
    {
        Image sr = GetComponent<Image>();
        Color originalColor = sr.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        gameObject.SetActive(false); // Desactiva el objeto al final
    }
}
