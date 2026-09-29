using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PanelGameOver : MonoBehaviour
{
    [Header("Variables que Cambian")]
    public GameObject panelOver;
    public GameObject imageWin;
    public GameObject imageLose;
    public TextMeshProUGUI tittle;
    public TextMeshProUGUI resultados;
    public TextMeshProUGUI notaUI;
    string informacionNota;
    string winOrLose;

    [Header("Booleanos Panel")]
    public bool isActivePanel;
    public bool isImageWin;
    public bool isGenerateNota;

    [Header("Variables Nota")]
    public float puntajeObtenido;
    public float notaFinal;
    public int cantidadPreguntas;
    public int cantidadPreguntasBoss;
    public int respuestasCorrectas = 0;
    public int respuestasInCorrectas = 0;
    

    [Header("Variables Generador Notas")]
    public float nota;
    public float notaApr;
    public float notaMin;
    public float notaMax;
    public float puntajeMax;
    public float puntaje;
    public float escala;

    public float decimal2;

    bool isNotaA;
    bool isNotaB;

    public static PanelGameOver instance;
    private void Start()
    {
        if (instance != null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        puntajeObtenido = respuestasCorrectas;

        //panelOver.SetActive(false);
        if (isGenerateNota)
        {
            if (puntaje < ((escala / 100) * puntajeMax))
            {
                isNotaA = true;
            }
            else
            {
                isNotaB = false;
            }

            GeneradorNotas(isNotaA);
        }
    }

    public void NotasPManager()
    {
        //cantidadPreguntasBoss = pManager.cantidadAExtraerBoss;

        puntajeMax = cantidadPreguntas + cantidadPreguntasBoss;
        puntaje = respuestasCorrectas;
    }

    public void GeneradorNotas(bool isNota)
    {
        NotasPManager();
        // Regla matematica para calcular la nota obtenida !
        if (isNota)
        {

            nota = ((notaApr - notaMin) * (puntaje / ((escala / 100) * puntajeMax)) + notaMin);
        }
        if (!isNota)
        {
            //Debug.LogWarning("Estoy en nota=False");
            nota = (notaMax - notaApr) * ((puntaje - ((escala / 100) * puntajeMax)) / (puntajeMax * (1 - (escala / 100)))) + notaApr;
        }
        //RedondearNota(nota * 10);
        notaFinal=RedondearNota(nota);
        InformacionFinal();
    }

    public float RedondearNota(float valor)
    {
        return (float)Math.Round(valor,1,MidpointRounding.AwayFromZero);
                
    }

    public void InformacionFinal()
    {
        if (notaFinal < 4)
        {
            informacionNota = "Lo siento, no alcansaste la nota minima de aprobacion, tu Nota es: ";

        }
        if (notaFinal >= 4 && notaFinal < 6)
        {
            informacionNota = "Muy bien, aprobaste con una Nota: ";
        }

        if (notaFinal >= 6 && notaFinal < 7)
        {
            informacionNota = "Exelente, eres de los mejores, tu Nota es:";
        }

        if (notaFinal >= 7)
        {
            informacionNota = " Eres el mejor, tu nota es Perfecta: ";

        }

        resultados.text = informacionNota;

        int valorEntero = Mathf.FloorToInt(notaFinal);
        float valorRestante = valorEntero - notaFinal;
        decimal2 = valorRestante;

        if (valorRestante ==0)
        {
            notaUI.text = "" + notaFinal + ",0";
        }
        else
        {
            notaUI.text = "" + notaFinal;
        }

        float notaFinalEnteros = notaFinal * 10;
        tittle.text = "YOU WIN";

           // Activar esto despues, solo por ahora esta desabilitado
        //ValidadorDeNombre.instance.GuardarPuntuacionFinal((int)notaFinalEnteros);

    }

    public void InicioPanel()
    {
        panelOver.SetActive(true);
        isGenerateNota = true;
    }
}
