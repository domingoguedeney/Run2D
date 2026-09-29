using System.Collections;
using UnityEngine;

public class EfectoMuerteCirculo : MonoBehaviour
{
    [Header("Referencias UI")]
    public RectTransform mascaraCirculo; // Asigna aquí el objeto 'TransicionMuerte'
    public Transform jugador;            // Asigna aquí el Transform de tu Personaje

    [Header("Configuración")]
    public float velocidadCierre = 2f;  // Tiempo en segundos que tarda en cerrarse
    public float escalaInicial = 15f;    // Tamaño inicial para cubrir pantallas grandes

    private Camera camaraPrincipal;

    void Start()
    {
        camaraPrincipal = Camera.main;
        // Inicialmente el círculo es gigante para que se vea todo el juego
        mascaraCirculo.localScale = new Vector3(escalaInicial, escalaInicial, 1f);
        IniciarEfectoMuerte();
    }

    // Llama a esta función pública desde tu script de daño/muerte del personaje
    public void IniciarEfectoMuerte()
    {
        StartCoroutine(CerrarCirculo());
    }

    private IEnumerator CerrarCirculo()
    {
        // 1. Mover la máscara a la posición de la pantalla donde está el jugador
        Vector2 posicionPantalla = camaraPrincipal.WorldToScreenPoint(jugador.position);
        mascaraCirculo.position = posicionPantalla;

        // 2. Reducir la escala de la máscara gradualmente hasta 0
        float tiempoTranscurrido = 0f;
        Vector3 escalaInicialVector = new Vector3(escalaInicial, escalaInicial, 1f);

        while (tiempoTranscurrido < 1f)
        {
            tiempoTranscurrido += Time.deltaTime * velocidadCierre;

            // Interpolación lineal de la escala hacia cero
            mascaraCirculo.localScale = Vector3.Lerp(escalaInicialVector, Vector3.zero, tiempoTranscurrido);

            yield return null;
        }

        // Asegurar que se cierre por completo
        mascaraCirculo.localScale = Vector3.zero;

        // Aquí puedes desencadenar tu lógica de Game Over o reiniciar nivel
        Debug.Log("Pantalla totalmente en negro. Reiniciando...");
    }
}
