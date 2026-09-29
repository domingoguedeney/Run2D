using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FilaUI : MonoBehaviour
{
    [Header("Referencias de Texto")]
    public TextMeshProUGUI textoPuesto;
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoPuntos;

    [Header("Configuración de Colores")]
    public Color colorOro = new Color(1f, 0.84f, 0f);    // Dorado
    public Color colorPlata = new Color(0.75f, 0.75f, 0.75f); // Plateado
    public Color colorBronce = new Color(0.8f, 0.5f, 0.2f);   // Bronce
    public Color colorNormal = Color.white;

    public void Configurar(int puesto, string nombre, int puntos)
    {
        // 1. Asignar los valores a los textos
        textoPuesto.text = puesto.ToString() + "°";
        textoNombre.text = nombre;
        textoPuntos.text = puntos.ToString();

        // 2. Aplicar colores especiales según el puesto
        Color colorFinal;

        switch (puesto)
        {
            case 1:
                colorFinal = colorOro;
                // Opcional: Podrías hacer el texto un poco más grande para el ganador
                textoNombre.fontWeight = FontWeight.Bold;
                break;
            case 2:
                colorFinal = colorPlata;
                break;
            case 3:
                colorFinal = colorBronce;
                break;
            default:
                colorFinal = colorNormal;
                textoNombre.fontWeight = FontWeight.Regular;
                break;
        }

        // Aplicamos el color a todos los elementos de la fila
        textoPuesto.color = colorFinal;
        textoNombre.color = colorFinal;
        textoPuntos.color = colorFinal;
    }
}
