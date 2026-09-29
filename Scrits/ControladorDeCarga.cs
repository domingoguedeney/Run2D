using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Nota: Si usas TextMeshPro, añade "using TMPro;" y cambia "Text" por "TextMeshProUGUI"

public class ControladorDeCarga : MonoBehaviour
{
    [Header("Elementos de la Interfaz")]
    public GameObject panelDeCarga;
    public Slider barraDeProgreso;
    public TextMeshProUGUI textoPorcentaje;

    [Header("Configuracion de Tiempo")]
    public float tiempoMinimoDeCarga = 3.0f; // Segundos que durara como minimo


    /// <summary>
    /// Llama a este método desde un botón o evento para iniciar la carga.
    /// </summary>
    /// <param name="indiceEscena">El número de la escena en el Build Settings</param>
    public void IniciarCarga(int indiceEscena)
    {
        // 1. Mostrar la pantalla de carga
        panelDeCarga.SetActive(true);

        // 2. Iniciar la corrutina que carga el nivel en segundo plano
        StartCoroutine(CargarEscenaAsincrona(indiceEscena));
    }

    private IEnumerator CargarEscenaAsincrona(int indice)
    {
        // Comenzamos la operación asíncrona
        AsyncOperation operacion = SceneManager.LoadSceneAsync(indice);

        // Opcional: Evita que la escena se active instantáneamente al llegar al 100%
        operacion.allowSceneActivation = false;

        float tiempoTranscurrido = 0f;
        
        while (tiempoTranscurrido<tiempoMinimoDeCarga || operacion.progress<0.9f)
        {
            tiempoTranscurrido += Time.deltaTime;

            // Calculamos el progreso visual basado en el tiempo o la carga real (el que vaya más lento)
            float progresoVisual = Mathf.Clamp01(tiempoTranscurrido / tiempoMinimoDeCarga);
            float progresoReal = Mathf.Clamp01(operacion.progress / 0.9f);

            // Usamos el valor más bajo para que la barra no se adelante a la carga real
            float progresoFinal = Mathf.Min(progresoVisual, progresoReal);

            // Actualizamos la barra de carga
            if (barraDeProgreso != null)
                barraDeProgreso.value = progresoFinal;

            // Actualizamos el porcentaje del texto
            if (textoPorcentaje != null)
            {
                // Multiplicamos por 100 para tener un valor del 0 al 100 y redondeamos
                textoPorcentaje.text = Mathf.RoundToInt(progresoFinal * 100f) + "%";
            }

            yield return null;
        }

        // Una vez cumplido el teimpo y la carga, activamos la escena
        operacion.allowSceneActivation = true;
       
    }
}