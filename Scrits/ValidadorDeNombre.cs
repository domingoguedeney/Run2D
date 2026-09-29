using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.Collections;
using UnityEngine.Networking;
//using UnityEditor.Experimental.GraphView;

public class ValidadorDeNombre : MonoBehaviour
{
    [Header("Activar o Desactivar Validador")]
    public bool isActive;
    
    [Header("Curso Seleccionado")]
    public bool quintoA;
    public bool quintoB;
    public bool quintoC;
    public bool quintoD;

    [Header("Menu Start")]
    public GameObject menuStart;
    public GameObject menuMute;

    [Header("UI RANKING")]
    public GameObject rankedMenu;
    public GameObject filaPrefab; // Arrastra aqui tu prefab de fila
    public Transform contenedorFilas; // El objeto con el Vertical Layout Group
    public bool isActiveRanking;

    [Header("Referencias UI")]
    public TMP_InputField inputNombre;
    public Button botonContinuar;
    public TextMeshProUGUI mensajeFeedback; // Opcional: para avisar que el nombre ya fue usado
    public CambioNivel cambioN;

    [Header("Base de Datos")]
    // Aqui escribes "Nombre apellido" tal cual
    public List<string> nombresAutorizados = new List<string>
    {
        "Catalina Altamirano",
        "Mauricio Riquelme",
        "Daniela Brantt",
        "Jorge Brantt",
        "Cristina Hidalgo"
    };
    public List<string> nombreAutorizados5A = new List<string> { };
    public List<string> nombreAutorizados5B = new List<string> { };
    public List<string> nombreAutorizados5C = new List<string> { };
    public List<string> nombreAutorizados5D = new List<string> { };
    


    [Header("Conexion Servidor")]
    public string urPHP = "http://192.168.100.106:8195/wp-admin/registro_unity.php";
    public string claveSeguridad = "mauricio1982";

    public static ValidadorDeNombre instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // El botón empieza desactivado
        botonContinuar.interactable = false;

        // Escuchamos cada vez que el usuario escribe una letra
        inputNombre.onValueChanged.AddListener(ValidarEntrada);
    }
    private void Update()
    {
        if (quintoA)
        {
           nombresAutorizados = nombreAutorizados5A;
        }
        if (quintoB)
        {
            nombresAutorizados = nombreAutorizados5B;
        }
        if (quintoC)
        {
            nombresAutorizados = nombreAutorizados5C;
        }
        if (quintoD)
        {
            nombresAutorizados = nombreAutorizados5D;
        }


        if (!isActiveRanking)
        {
            rankedMenu.SetActive(false);

        }
        else
        {
            rankedMenu.SetActive(true);
            ObtenerRanking();
        }
    }
    public void ValidarEntrada(string textoBruto)
    {
        // 0. Quitamos espacios al inicio y al final
        string nombreProcesado = LimpiarTexto(textoBruto);
        string idNombre = nombreProcesado.ToLower();

        // 1. Verificamos si el nombre esta en la lista de permitidos
        bool estaEnLista = nombresAutorizados.Any(n => n.Equals(nombreProcesado, System.StringComparison.OrdinalIgnoreCase));

        // 2. Verificar si el nombre YA fue usado anteriormente
        // Usamos una llave tipo "Usado_Juan Walker"
        bool yaFueUsado = PlayerPrefs.HasKey("Fecha_" + idNombre);

        if (estaEnLista && yaFueUsado)
        {
            // Recuperamos la fecha guardada para mostrarla
            string fechaRegistro = PlayerPrefs.GetString("Fecha_" + idNombre);
            mensajeFeedback.text = $"Este nombre ya se registro el: {fechaRegistro}";
            mensajeFeedback.color = Color.red;
            botonContinuar.interactable = false;

        }
        else if (estaEnLista && !yaFueUsado)
        {
            mensajeFeedback.text = "Nombre disponible para registro. ";
            mensajeFeedback.color = Color.green;
            botonContinuar.interactable = true;
        }
        else
        {
            mensajeFeedback.text = "";
            botonContinuar.interactable = false;
        }
    }

    public void ConfirmarRegistro()
    {


        string nombreFinal = LimpiarTexto(inputNombre.text);
        string idNombre = nombreFinal.ToLower();


        // 0. Capturar la fecha y hora actual
        // "dd/MM/yyyy HH:mm" -> Ejemplo: 30/04/2026 14:30
        string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


        // 1. Guardar Datos
        PlayerPrefs.SetString("NombreJugador", nombreFinal);
        PlayerPrefs.SetString("Fecha_" + idNombre, fechaActual);

        // 2. Marcamos este nombre como "usado" para siempre en este dispositivo
        PlayerPrefs.SetInt("Usado_" + nombreFinal.ToLower(), 1);

        PlayerPrefs.Save();

        Debug.Log($"Registro exitoso: {nombreFinal} ha sido marcado como usado");

        // Cargar siguiente escena...
        GetComponent<CambioNivel>().OnWebButton();
    }

    private string LimpiarTexto(string texto)
    {
        string resultado = texto.Trim();
        return Regex.Replace(resultado, @"\s+", " ");
    }

    // --- FUNCIÓN PARA PRUEBAS ---
    [ContextMenu("Borrar datos Guardados")]
    public void BorrarDatos()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Todos los nombres han sido liberados (PlyerPrefs borrado).");
    }


    public void ConfirmarYEnviar()
    {
        string nombreFinal = inputNombre.text.Trim();
        string fechaActual = System.DateTime.Now.ToString("dd//MM/yyyy HH:mm:ss");
        PlayerPrefs.SetString("NombreJugador", nombreFinal);
        PlayerPrefs.SetString("Fecha_" + nombreFinal.ToLower(), fechaActual);
        PlayerPrefs.Save();

        StartCoroutine(EnviarAWordPress(nombreFinal, fechaActual));
    }

    IEnumerator EnviarAWordPress(string nombre, string fecha)
    {
        WWWForm form = new WWWForm();
        form.AddField("clave", claveSeguridad);
        form.AddField("nombre_jugador", nombre);
        form.AddField("fecha_registro", fecha);

        using (UnityWebRequest www = UnityWebRequest.Post(urPHP, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Servidor dice: " + www.downloadHandler.text);
                // Si todo salio bien, aqui puedes llamar a tu pantalla de carga
                cambioN.OnWebButton();
                //GetComponent<CambioNivel>().OnWebButton();
                // Getcomponent<ControladorDeCarga>().IniicarCarga(1);
            }
            else
            {
                Debug.LogError("Error al conectar: " + www.error);
            }
        }
    }


    public void GuardarPuntuacionFinal(int puntosLogrados)
    {
        // Recuperamos el nombre que guardamos al inicio en PlayerPrefs
        string nombreGuardado = PlayerPrefs.GetString("NombreJugador", "");

        if (!string.IsNullOrEmpty(nombreGuardado))
        {
            StartCoroutine(EnviarPuntosAWordPress(nombreGuardado, puntosLogrados));
        }
    }

    IEnumerator EnviarPuntosAWordPress(string nombre, int puntos)
    {
        WWWForm form = new WWWForm();
        form.AddField("clave", claveSeguridad);
        form.AddField("nombre_jugador", nombre);
        form.AddField("puntos", puntos); // Enviamos los puntos

        using (UnityWebRequest www = UnityWebRequest.Post(urPHP, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Puntos guardados: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error al guardar puntos: " + www.error);
            }
        }
    }


    public void ObtenerRanking()
    {
        StartCoroutine(DescargarRankingCoroutine());
    }

    IEnumerator DescargarRankingCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("clave", claveSeguridad);
        form.AddField("accion", "GET_RANKING");

        using (UnityWebRequest www = UnityWebRequest.Post(urPHP, form))
        {
            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.Success)
            {
                string json = www.downloadHandler.text;
                // Convertimos el texto JSON en objetos C#
                ListaRanking ranking = JsonUtility.FromJson<ListaRanking>(json);
                MostrarRankingEnUI(ranking);
            }
            else
            {
                Debug.LogError("Error al obtener ranking: "+ www.error);
            }
            
        }
    } 

    void MostrarRankingEnUI(ListaRanking lista)
    {
        // 1. Limpiar el ranking anterior
        foreach(Transform hijo in contenedorFilas)
        {
            Destroy(hijo.gameObject);
        }

        // 2. Crear las nuevas filas
        int puesto = 1;
        foreach(var jugador in lista.jugadores)
        {
            GameObject nuevaFila = Instantiate(filaPrefab, contenedorFilas);

            // Asumiendo que tu prefab tiene un script "FilaRanking" para asignar textos
            nuevaFila.GetComponent<FilaUI>().Configurar(puesto, jugador.nombre_jugador, jugador.puntuacion);
            puesto++;
        }
    }

    public void OnRankedButton()
    {
        menuStart.SetActive(false);
        menuMute.SetActive(false);
        isActiveRanking = true;
    }

    public void OnStarMenuButton()
    {
        isActiveRanking = false;
        menuStart.SetActive(true);
        menuMute.SetActive(true);
        
    }
}
[System.Serializable]
public class DatosJugadoresRanking
{
    public string nombre_jugador;
    public int puntuacion;
}

[System.Serializable]
public class ListaRanking
{
    public DatosJugadoresRanking[] jugadores;
}