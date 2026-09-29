using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogoManager2 : MonoBehaviour
{
    public GameObject boxDialogo;
    public TextMeshProUGUI textoUI;
    public AudioSource fuenteAudio;
    public AudioClip sonidoLetra;
    public float velocidadEscritura = 0.05f;
    public int count;
    public Queue<string> frases = new Queue<string>();
    private bool estaEscribiendo = false;
    private string fraseActual = "";

    public bool isAvance;
    public bool isAttack;

    [TextArea(3, 5)]
    public string[] frase;


    public static DialogoManager2 instance;
    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        //IniciarDialogo(frase);
    }
    // Llama a esto para iniciar una conversación
    public void IniciarDialogo(string[] conversacion)
    {
        frases.Clear();
        //boxDialogo.SetActive(true);
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

            // SONIDO: Reproduce el clip en cada letra
            if (sonidoLetra && fuenteAudio)
            {
                // ------ EFECTO DE PITCH ALEATORIO ------
                // Cambiar el tono ligeramente en cada letra (0.9f a 1.1f es lo ideal.
                fuenteAudio.pitch = Random.Range(0.85f, 1.15f);

                fuenteAudio.PlayOneShot(sonidoLetra);
            }
            yield return new WaitForSeconds(velocidadEscritura);
        }
        // Al terminar, reseteamos el pitch a la normalidad
        fuenteAudio.pitch = 1f;
        estaEscribiendo = false;
    }

    void Update()
    {
        if (isAvance)
        {
            SiguienteFrase();
            isAvance = false;
        }

        if (isAttack)
        {
            StartCoroutine(InicioAttack());
            //BossMovimiento.instance.shootingBullet();
            //isAttack = false;
        }
        //if (Input.GetKeyDown(KeyCode.Space)) SiguienteFrase();
    }
    IEnumerator InicioAttack()
    {
        isAttack = false;
        yield return new WaitForSeconds(1);
        BossMalito.instance.isShooting = true;
        //BossMovimiento.instance.isShooting = true;
        //BossMovimiento.instance.shootingBullet();
        //isAttack = false;
    }
    public void Inicio()
    {
        StartCoroutine(EsperaDialogo());
    }

    public void BotonAvanceExit()
    {
        count++;  

        switch (count)
        {
            case 1:
                SiguienteFrase();
        break;
                case 2:
                boxDialogo.SetActive(false);
                isAttack = true;
                break;
        }
     
    }

    IEnumerator EsperaDialogo()
    {
        yield return new WaitForSeconds(0.5f);
        boxDialogo.SetActive(true);
        IniciarDialogo(frase);
    }
}
