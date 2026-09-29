using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PruebaBarra : MonoBehaviour
{
    [Header("Corazones (10 en total")]
    public GameObject[] corazones;

    private int corazonesActivos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        corazonesActivos=corazones.Length;
        ActualizarCorazones();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void QuitarCorazon()
    {
        if(corazonesActivos>0)
        {
            corazonesActivos--;
            ActualizarCorazones();
        }
    }

    public void AgregarCorazones()
    {
        if(corazonesActivos< corazones.Length)
        {
            corazonesActivos++;
            ActualizarCorazones();
        }
    }

    private void ActualizarCorazones()
    {
        for(int i = 0;i < corazones.Length; i++)
        {
            //corazones[i].SetActive(i < corazonesActivos);
            corazones[i].GetComponent<Image>().enabled=(i<corazonesActivos) ;
        }
    }
}
