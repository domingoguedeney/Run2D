using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class PregutaManager : MonoBehaviour
{
    [Header("Selecion Boss o Enemy")]
    public bool isBoss;
    public bool isBossP;


    public PanelGameOver panelOver;

    [Header("Bool Preguntas")]
    public bool preguntas;

    [Header("Configuracion")]
    public Questions[] posiblesObjetos;
    public Questions[] posiblesObjetosB;
    [Range(1, 20)] public int cantidadAExtraer = 3; //Cantidad de preguntas add a la lista
    [Range(1, 20)] public int cantidadAExtraerBoss = 3; //Cantidad de preguntas add a la lista Boss

    [Header("Lista de las preguntas Seleccionadas")]
    public List<Questions> miListaDeDatos = new List<Questions>();
    //public List<Questions> miListaDeDatosBoss = new List<Questions>();

    [Header("Pregunta Selecionada")] // Aqui estara la pregunta seleccionada 
    //public List<Questions> questions;
    public Questions questionSelected;
    //public Questions questionSelectedBoss;

    [Header("Configuracion UI")]
    [SerializeField] private TextMeshProUGUI pregunta;
    [SerializeField] private Button[] boton;
    public bool isCorrect1;
    public bool isCorrect2;
    public bool isCorrect3;

    [SerializeField] private string respuesta1;
    [SerializeField] private string respuesta2;
    [SerializeField] private string respuesta3;

    int numCorrecta;
    GameObject botonClikeado;
    int index;
    public GameObject panel;

    public bool listaVacia => miListaDeDatos.Count == 0;
    //public bool listaVaciaBoss => miListaDeDatosBoss.Count == 0;

    public static PregutaManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (!preguntas)
        {

            if (isBoss)
            {
                cantidadAExtraerBoss = AddPregunta.instance.numPreguntas;
            }
            else
            {
                cantidadAExtraer = AddPregunta.instance.numPreguntas;
            }

        }


        AgregarAlAzar(cantidadAExtraer);


    }
    // Update is called once per frame
    void Update()
    {
        panelOver.cantidadPreguntas = cantidadAExtraer;
        panelOver.cantidadPreguntasBoss = cantidadAExtraerBoss;

        //PanelGameOver.instance.cantidadPreguntas = cantidadAExtraer;
        if (isBossP)
        {
            AgregarAlAzar(cantidadAExtraerBoss);
            isBossP = false;

        }
        if (listaVacia)
        {
            Debug.Log("La lista esta VACIA. !");
            if (isBoss)
            {
                //BossMovimiento.instance.isEmpy = true;
                BossMalito.instance.isEmpy = true;
                //panelOver.InicioPanel();
            }
            else
            {
                SpawnPregunta.instance.isEmpy = true;
                //Movimiento_Player.instance.velocidadMovimiento = 0; }

            }
        }
    }


    public void RegistrarObjeto(Questions obj)
    {
        if (!miListaDeDatos.Contains(obj))
        {
            miListaDeDatos.Add(obj);
        }
        else
        {
            //No pasa nada...
        }

    }

    public void AgregarAlAzar(int cantidad)
    {
        if (isBoss)
        {

            //1. Creamos una copia de los candidatos para no alterar el array
            List<Questions> copiaCandidatos = new List<Questions>(posiblesObjetosB);

            //2. Validamos que no pidamos mas objetos de los que existen
            int limite = Mathf.Min(cantidad, copiaCandidatos.Count);

            for (int i = 0; i < limite; i++)
            {
                //3. Elegimos uno al azar de la copia
                int indiceRandom = Random.Range(0, copiaCandidatos.Count);
                Questions elegido = copiaCandidatos[indiceRandom];

                //4. Lo enviamos 
                RegistrarObjeto(elegido);

                //5. ¡Importante! Lo eliminamos de la copia para que no salga repetido
                copiaCandidatos.RemoveAt(indiceRandom);
            }
        }
        else
        {
            //1. Creamos una copia de los candidatos para no alterar el array
            List<Questions> copiaCandidatos = new List<Questions>(posiblesObjetos);

            //2. Validamos que no pidamos mas objetos de los que existen
            int limite = Mathf.Min(cantidad, copiaCandidatos.Count);

            for (int i = 0; i < limite; i++)
            {
                //3. Elegimos uno al azar de la copia
                int indiceRandom = Random.Range(0, copiaCandidatos.Count);
                Questions elegido = copiaCandidatos[indiceRandom];

                //4. Lo enviamos 
                RegistrarObjeto(elegido);

                //5. ¡Importante! Lo eliminamos de la copia para que no salga repetido
                copiaCandidatos.RemoveAt(indiceRandom);
            }
        }
        
    }

    public void LimpiarLista()
    {
        miListaDeDatos.Clear();
    }


    public void AlHacerClic()
    {
        botonClikeado = EventSystem.current.currentSelectedGameObject;
        Debug.LogWarning("Nombre del boton: " + botonClikeado.name);


    }


    public void Prueba()
    {
        //Verificar que aun quedan objetos
        if (miListaDeDatos.Count > 0) //if (questions.Count > 0)
        {
            // obtener un indice aleatorio
            int randomIndex = Random.Range(0, miListaDeDatos.Count);

            questionSelected = miListaDeDatos[randomIndex];

            pregunta.text = questionSelected.pregunta;
            respuesta1 = questionSelected.opciones[0].opcion;
            respuesta2 = questionSelected.opciones[1].opcion;
            respuesta3 = questionSelected.opciones[2].opcion;

            //questions.RemoveAt(randomIndex);
            miListaDeDatos.RemoveAt(randomIndex);
            Debug.Log("Estoy despues de Remover randomIndex. !");
        }
    }
    public void ScanPreguntas(int numero)
    {
        ActivarButtons();

        switch (numero)
        {

            case 0:
                Debug.Log("Pase por la opcion 0 !");
                isCorrect1 = false;
                isCorrect2 = false;
                isCorrect3 = false;

                boton[0].image.color = Color.white;
                boton[1].image.color = Color.white;
                boton[2].image.color = Color.white;

                boton[0].onClick.RemoveAllListeners();
                boton[1].onClick.RemoveAllListeners();
                boton[2].onClick.RemoveAllListeners();

                //quest = null;
                break;
            case 1:
                panel.SetActive(true);

                //quest = FindAnyObjectByType<Question>().questionSelected;

                if (questionSelected != null) // Diagrama mas o menos para el boleano iscorrect en los botones del menu
                {
                    Debug.Log("estoy en el Scan");
                    pregunta.text = questionSelected.pregunta;


                    if (questionSelected.opciones[0].iscorrecta)
                    {
                        isCorrect1 = true;
                        numCorrecta = 0;

                        boton[0].onClick.AddListener(respuestaCorrecta);
                        boton[1].onClick.AddListener(respuestasIncorrectas);
                        boton[2].onClick.AddListener(respuestasIncorrectas);


                    }
                    else if (questionSelected.opciones[1].iscorrecta)
                    {
                        isCorrect2 = true;
                        numCorrecta = 1;

                        boton[1].onClick.AddListener(respuestaCorrecta);
                        boton[2].onClick.AddListener(respuestasIncorrectas);
                        boton[0].onClick.AddListener(respuestasIncorrectas);

                    }
                    else if (questionSelected.opciones[2].iscorrecta)
                    {
                        isCorrect3 = true;
                        numCorrecta = 2;

                        boton[2].onClick.AddListener(respuestaCorrecta);
                        boton[0].onClick.AddListener(respuestasIncorrectas);
                        boton[1].onClick.AddListener(respuestasIncorrectas);

                    }
                    boton[0].GetComponentInChildren<TextMeshProUGUI>().text = questionSelected.opciones[0].opcion;
                    boton[1].GetComponentInChildren<TextMeshProUGUI>().text = questionSelected.opciones[1].opcion;
                    boton[2].GetComponentInChildren<TextMeshProUGUI>().text = questionSelected.opciones[2].opcion;
                }
                break;

        }
    }


    // TIPOS DE RESPUESTAS (CORRECTAS & INCORRECTAS
    public void respuestasIncorrectas()
    {
        Debug.LogWarning("Respuesta incorrecta! .");

        DesactiveButtons();
        if (boton[0].onClick != null)
        {
            if (isCorrect1 == false)
            {
                botonClikeado.GetComponent<Button>().image.color = Color.red;
            }
        }
        if (boton[1].onClick != null)
        {
            if (isCorrect2 == false)
            {
                botonClikeado.GetComponent<Button>().image.color = Color.red;
            }

        }
        if (boton[2].onClick != null)
        {
            if (isCorrect3 == false)
            {
                botonClikeado.GetComponent<Button>().image.color = Color.red;
            }
        }

        Movimiento_Player.instance.isCorrecta(false);


    }

    public void respuestaCorrecta()
    {
        Debug.LogWarning("Respuesta Correcta! ." + boton[numCorrecta].name);
        DesactiveButtons();
        if (boton[0].onClick != null)
        {
            if (isCorrect1)
            {
                boton[0].image.color = Color.green;
            }
        }
        if (boton[1].onClick != null)
        {
            if (isCorrect2)
            {
                boton[1].image.color = Color.green;
            }

        }
        if (boton[2].onClick != null)
        {
            if (isCorrect3)
            {
                boton[2].image.color = Color.green;
            }
        }

        Movimiento_Player.instance.isCorrecta(true);
    }


    public void DesactiveButtons()
    {
        for (int i = 0; i < boton.Length; i++)
        {
            boton[i].interactable = false;
        }
    }

    public void ActivarButtons()
    {
        for (int i = 0; i < boton.Length; i++)
        {
            boton[i].interactable = true;
        }
    }
}
