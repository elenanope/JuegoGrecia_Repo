using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    [Header("Text References")]
    [SerializeField] TMPro.TMP_Text playerText;//Cambiar por panel de tres puntos
    [SerializeField] TMPro.TMP_Text otherText;

    [Header("Dialogo Arrays")]
    public string[] characterDialog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
