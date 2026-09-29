using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class AnimacionEscrtura : MonoBehaviour
{
    public TextMeshProUGUI textoUI;
    public AudioSource fuenteAudio;
    public AudioClip sonidoLetra;
    public float velocidadEscritura = 0.05f;
    public Queue<string> frases = new Queue<string>();
    private bool estaEscribiendo = false;
    private string fraseActual = "";


    [TextArea(3, 5)]
    public string[] frase;

    private void Start()
    {
        IniciarDialogo(frase);
    }
    // Llama a esto para iniciar una conversación
    public void IniciarDialogo(string[] conversacion)
    {
        frases.Clear();
        foreach (string frase in conversacion) frases.Enqueue(frase);
        SiguienteFrase();
    }

    public void SiguienteFrase()
    {
        if (estaEscribiendo)
        {
            // SALTAR ANIMACIÓN: Si ya está escribiendo, muestra todo de golpe
            StopAllCoroutines();
            textoUI.text = fraseActual;
            estaEscribiendo = false;
            return;
        }

        if (frases.Count == 0) return; // Fin de la charla

        fraseActual = frases.Dequeue();
        StartCoroutine(EscribirFrase(fraseActual));
    }

    private IEnumerator EscribirFrase(string frase)
    {
        estaEscribiendo = true;
        textoUI.text = "";

        foreach (char letra in frase.ToCharArray())
        {
            textoUI.text += letra;

            if (sonidoLetra != null)
            {
                // SONIDO: Reproduce el clip en cada letra
                if (sonidoLetra && fuenteAudio)
                {
                    // ------ EFECTO DE PITCH ALEATORIO ------
                    // Cambiar el tono ligeramente en cada letra (0.9f a 1.1f es lo ideal.
                    fuenteAudio.pitch = Random.Range(0.85f, 1.15f);

                    fuenteAudio.PlayOneShot(sonidoLetra);
                }
            }
            yield return new WaitForSeconds(velocidadEscritura);
        }
        // Al terminar, reseteamos el pitch a la normalidad
        if(sonidoLetra!=null) fuenteAudio.pitch = 1f;
        estaEscribiendo = false;
    }

}
