using System.Collections.Generic;
using UnityEngine;
using UnityExtensions;

public class DialogueSpeaker : MonoBehaviour
{
    //[ReorderableList]
    public Conversacion conversacion;
    //public Preguntas pregunta;
    //public DialogoUI dialogo;
    //public GameObject dia;


    void Start()
    {
        //dialogo=FindAnyObjectByType<DialogoUI>();
        //dialogo = GameObject.Find("DialogoUI").GetComponent<DialogoUI>();
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Conversar();

        }

    }

    public void Conversar()
    {
        DialogoManager.instance.MostrarUI(true);

        DialogoUI.instance.conversacion= conversacion;
        //dialogo.conversacion= conversacion;
        DialogoUI.instance.ActualizarTextos();
        Debug.LogWarning("Paso por Conversar!!");
        
        //dialogo.ActualizarTextos();
        
    }

   

}
