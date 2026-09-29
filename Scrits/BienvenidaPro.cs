using System;
using TMPro;
using UnityEngine;

public class BienvenidaPro : MonoBehaviour
{
    //  -------------------------------------  SCRIPT PARA SALUDAR A JUGADOR Y MOSTRARLE EL TIEMPO QUE ESTUVO SIN ENTRAR AL JUEGO  -------------------------------------------------------------
    [Header("Configuracion")]
    public bool mostrarTiempoTranscurrido = true;
    public TextMeshProUGUI textoSaludo;

    void Start()
    {
        MostrarSaludo();    
    }

    void MostrarSaludo()
    {
        string nombre = PlayerPrefs.GetString("NombreJugador", "Detective");
        string idNombre= nombre.ToLower();

        if(!mostrarTiempoTranscurrido || !PlayerPrefs.HasKey("Fecha_"+ idNombre))
        {
            textoSaludo.text = $"Bienvenido de nuevo, {nombre}.";
            return;
        }

        // Recuperar la fecha y calcular la diferencia
        string fechaString = PlayerPrefs.GetString("Fecha_" + idNombre);
        DateTime fechaRegistro = DateTime.ParseExact(fechaString, "dd//MM/yyyy HH:mm:ss", null);
        TimeSpan tiempoTranscurrido=DateTime.Now - fechaRegistro;

        // Formatear el mensaje de tiempo
        string mensajeTiempo = FormatearTiempo(tiempoTranscurrido);
        textoSaludo.text = $"Hola {nombre}. Han pasado {mensajeTiempo} desde que te registraste. ";
        
    }

    string FormatearTiempo(TimeSpan t)
    {
        if (t.TotalDays >= 1) return $"{(int)t.TotalDays} dias";
        if (t.TotalHours >= 1) return $"{(int)t.TotalHours} horas";
        if (t.TotalMinutes >= 1) return $"{(int)t.TotalMinutes} minutos";
        return "unos pocos segundos";
    }
}
