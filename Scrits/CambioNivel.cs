using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambioNivel : MonoBehaviour
{
    [Header("Menu´s")]
    public GameObject starMenu;
    public GameObject selectMenu;
    public GameObject ConfiMenu;
    public GameObject nombreMenu;
    public bool isWeb=false;
    public bool isClass;
    ControladorDeCarga controlCarga;

    [Header("Numero de Preguntas")]
    public GameObject[] menuPreguntas;

    public GameObject[] menu;
    //public int menuEnu;

    GameObject botonClikeado;
    public Button[] button;

    public int unidad;

    PregutaManager pManager;
    int numBoton;
    string nameBoton;

    private void Start()
    {
        menuPreguntas[0].SetActive(false);
        menuPreguntas[1].SetActive(false);
        menuPreguntas[2].SetActive(false);
        menuPreguntas[3].SetActive(false);
        controlCarga = GetComponent<ControladorDeCarga>();
    }

    private void Update()
    {
        isWeb = ValidadorDeNombre.instance.isActive;
        switch (unidad)
        {
            case 1:
                button[0].interactable = true;
                button[1].interactable = false;
                button[2].interactable = false;
                button[3].interactable = false;
                break;
            case 2:
                button[0].interactable = true;
                button[1].interactable = true;
                button[2].interactable = false;
                button[3].interactable = false;
                break;
            case 3:
                button[0].interactable = true;
                button[1].interactable = true;
                button[2].interactable = true;
                button[3].interactable = false;
                break;
            case 4:
                button[0].interactable = true;
                button[1].interactable = true;
                button[2].interactable = true;
                button[3].interactable = true;
                break;
        }
    }

    public void ClickUnidad()
    {
        botonClikeado = EventSystem.current.currentSelectedGameObject;

        //Debug.LogWarning("este es el segundo menu y el nombre del boon es: "+ botonClikeado.name);
        switch (botonClikeado.name)
        {
            case "5":
                // Aqui va el codigo para ir al nivel, con el numero de preguntas
                AddPregunta.instance.CantiPreguntas(5);
                break;
            case "10":
                // Aqui va el codigo para ir al nivel, con el numero de preguntas
                AddPregunta.instance.CantiPreguntas(10);
                break;
            case "15":
                // Aqui va el codigo para ir al nivel, con el numero de preguntas
                AddPregunta.instance.CantiPreguntas(15);
                break;
        }
        SceneManager.LoadScene("Nivel_1");
        menu[1].SetActive(false);
    }
    public void AlHacerClic()
    {
        botonClikeado = EventSystem.current.currentSelectedGameObject;

        // Desactiva todas los menus de prejuntas
        for (int i = 0; i < menuPreguntas.Length; i++)
        {
            menuPreguntas[i].SetActive(false);
        }

        nameBoton = botonClikeado.name;

        // Identifica el id del botn y se lo asigna a la variable numBoton
        switch (nameBoton)
        {
            case "Unidad I":
                numBoton = 0;
                break;
            case "Unidad II":
                numBoton = 1;
                break;
            case "Unidad III":
                numBoton = 2;
                break;
            case "Unidad IV":
                numBoton = 3;
                break;
        }

        // Activa el menu segun el boton seleccionado
        menuPreguntas[numBoton].SetActive(true);
    }

    public void CambioScena(string nombre)
    {
        SceneManager.LoadScene(nombre);
        menu[1].SetActive(false);
        //menu[2].SetActive(false);
        //menu[3].SetActive(false);
    }

    public void CambioPanelStart()
    {
            //Estoy en menu Start
            menu[0].SetActive(true);
            menu[1].SetActive(false);
            //menu[2].SetActive(false);
    }
    public void CambioPanelSelect()
    {
        if (isWeb)
        {
            //SceneManager.LoadScene("Nivel_1");
            //controlCarga.IniciarCarga(1);
            nombreMenu.SetActive(true);
            menu[0].SetActive(false);
            menu[1].SetActive(false);
        }
        else if(!isClass) 
        {
            //Estoy en el menu Select
            menu[1].SetActive(true);
            // menu[2].SetActive(false);
            menu[0].SetActive(false);
        }
        else
        {
            controlCarga.IniciarCarga(1);
        }
         
    }

    // Esto solo funcionara en estado WEB
    public void OnWebButton()
    {
        controlCarga.IniciarCarga(1);
        nombreMenu.SetActive(false);
    }

    public void CambioPanelConfig()
    {
        //Estoy en el menu Configuracion
        //menu[2].SetActive(true);
        menu[0].SetActive(false);
        menu[1].SetActive(false);
    }

    public void CambioScena_Test()
    {
        Debug.Log("Esta Escena aun no esta activa!. ");
    }

}
