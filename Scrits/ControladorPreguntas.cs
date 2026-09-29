using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorPreguntas : MonoBehaviour
{
    [SerializeField] private GameObject buttonPref;
    [SerializeField] private TextMeshProUGUI pregText;
    [SerializeField] private Transform opcionesContainer;
    private List<Button> poolButtons= new List<Button> ();

    

    public void ActivarBotones(int cantidad, string title, Opciones[] opciones)
    {
        pregText.text = title;
        if (poolButtons.Count >= cantidad)
        {
            for (int i = 0; i < poolButtons.Count; i++)
            {
                if (i < cantidad)
                {
                    poolButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = opciones[i].opcion;
                    poolButtons[i].onClick.RemoveAllListeners();
                    Conversacion co = opciones[i].convResultante;
                    poolButtons[i].onClick.AddListener(() => DarFuncionesABotones(co));
                    poolButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    poolButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            int cantidadRestante = (cantidad - poolButtons.Count);
            for (int i = 0; i < cantidadRestante; i++)
            {
                var nextButton = Instantiate(buttonPref, opcionesContainer).GetComponent<Button>();
                nextButton.gameObject.SetActive(true);
                poolButtons.Add(nextButton);
            }
            ActivarBotones(cantidad, title, opciones);
        }
    }

    //public void DarFuncionesABotones(Conversacion conv)
    public void DarFuncionesABotones( Conversacion conv)
    {
        
        if (!conv.isBoss)
        {
            if (conv.isCorrecta)
            {
                Movimiento_Player.instance.isCorrecta(true);
               
                Debug.Log("Respuesta Correcta!!");
            }
            else
            {
                Movimiento_Player.instance.isCorrecta(false);
                
                Debug.Log("Respuesta Incorrecta!! ");

            }
        }
        else
        {
            if (conv.isCorrecta)
            {
                Movimiento_Player.instance.isInmune = true;
                //Falta el ataque del Boss
            }
            else
            {

            }

            //Aqui va lo que pasa cuando se el Boss el que ataca
        }
       
    }
        
}
