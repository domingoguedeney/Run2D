using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogoUI : MonoBehaviour
{
    public Conversacion conversacion;

    [SerializeField] private float textSpeed = 10;

    [SerializeField] private GameObject pregContainer;

    [SerializeField] private TextMeshProUGUI convText;

    private AudioSource audioSource;

    public static DialogoUI instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        pregContainer.SetActive(true);

    }

    private void Update()
    {

    }

    public void ActualizarTextos()
    {
        pregContainer.SetActive(true);

        ///////////////
        pregContainer.SetActive(true);
        var preg = conversacion.pregunta;

        DialogoManager.instance.controladorPreguntas.ActivarBotones(preg.opciones.Length, preg.pregunta, preg.opciones);


    }

    //IEnumerator EscribirTexto()
    //{
    //    convText.maxVisibleCharacters = 0;
    //    convText.text = conversacion.dialogos[localIn].dialogo;
    //    convText.richText = true;

    //    for (int i = 0; i < conversacion.dialogos[localIn].dialogo.ToCharArray().Length; i++)
    //    {
    //        convText.maxVisibleCharacters++;
    //        yield return new WaitForSeconds(1f / textSpeed);
    //    }
    //}
}
